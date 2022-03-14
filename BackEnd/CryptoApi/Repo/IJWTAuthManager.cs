using Dapper;
using Model;

namespace CryptoAPI
{
    public interface IJWTAuthManager
    {
        ResponseModel<string> GenerateJWT(AccountUser user);
        ResponseModel<T> Execute_Command<T>(string query, DynamicParameters sp_params);
    }
} 