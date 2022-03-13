using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.IdentityModel.Tokens;
using Model;

namespace CryptoAPI
{
    public class JWTAuthManager : IJWTAuthManager
     {
         private readonly IConfiguration _configuration;
  
         public JWTAuthManager(IConfiguration configuration)
         {
             _configuration = configuration;
         }
         public ResponseModel<string> GenerateJWT(AccountUser user)
         { 
             ResponseModel<string> response = new ResponseModel<string>();
  
             var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuth:Key"]));
             var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
  
             //claim is used to add identity to JWT token
             var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, user.username),
                 new Claim(JwtRegisteredClaimNames.Name, user.name),
                 new Claim("roles", Convert.ToString(user.isAdmin)),
                 new Claim("Date", DateTime.Now.ToString()),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };
  
             var token = new JwtSecurityToken(_configuration["JwtAuth:Issuer"],
               _configuration["JwtAuth:Issuer"],
               claims,    //null original value
               expires: DateTime.Now.AddMinutes(120),
               signingCredentials: credentials);
  
             response.Data = new JwtSecurityTokenHandler().WriteToken(token); //return access token
             response.code = 200;
             response.message = "Token generated";
             return response;
         }
  
         public ResponseModel<T> Execute_Command<T>(string query, DynamicParameters sp_params)
         {
             ResponseModel<T> response = new ResponseModel<T>();
             using (IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("Reference2DB")))
             {
                 if (dbConnection.State == ConnectionState.Closed)
                     dbConnection.Open();
                using var transaction = dbConnection.BeginTransaction();
                 try
                 {
                     response.Data = dbConnection.Query<T>(query, sp_params, commandType: CommandType.StoredProcedure, transaction: transaction).FirstOrDefault();
                     response.code = sp_params.Get<int>("retVal");
                     response.message = "Success";
                     transaction.Commit();
                 }
                 catch (Exception ex)
                 {
                     transaction.Rollback();
                     response.code = 500;
                     response.message = ex.Message;
                 }
             }
             return response;
         }
     }
} 