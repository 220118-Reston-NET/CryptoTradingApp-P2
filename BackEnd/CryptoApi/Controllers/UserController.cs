using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CryptoBL;
using System.Data.SqlClient;
using Model;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Dapper;
using System.Data;
using CryptoAPI;

namespace CryptoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IJWTAuthManager _authentication;
        private ICryptoClassBL _cryptoBL;
        public UserController(ICryptoClassBL p_cryptoBL, IJWTAuthManager authentication)
        {
            _cryptoBL = p_cryptoBL;
            this._authentication = authentication;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Parameter is missing");
            }
        
            DynamicParameters dp_param = new DynamicParameters();
            dp_param.Add("userName", user.username, DbType.String);
            dp_param.Add("userPassword", user.password, DbType.String);
            dp_param.Add("retVal", DbType.String, direction: ParameterDirection.Output);
            var result = _authentication.Execute_Command<AccountUser>("UserLogin",dp_param);
            if (result.code == 200)
            {
                var token = _authentication.GenerateJWT(result.Data);
                Log.Information(token.message);
                Log.Information("User has logged in successfully");
                return Ok(token);
            }
            Log.Warning(result.message);
            return NotFound(result.Data);
        }

        // GET: api/User
        [HttpPost("AddUser")]
        [AllowAnonymous]
        public IActionResult AddUser([FromBody] AccountUser p_NewUser)
        {
            return Ok(_cryptoBL.AddUser(p_NewUser));
            // try
            // {
            //     Log.Information("Added user successfully");
                
            // }
            // catch (SqlException)
            // {
            //     Log.Warning("Issue with add user function");
            //     return NotFound();
            // }
        }

        // GET: api/User/5
        [HttpPost("PlaceOrder")]
        [Authorize(Roles = "0,1")]
        public IActionResult PlaceOrder([FromBody] Tuple<Assets, BuyOrderHistory> p_tuple, decimal p_amount, int p_userID)
        {
            try
            {
                Log.Information("User has placed order successfully"); 
                _cryptoBL.PlaceOrder(p_tuple.Item1, p_amount, p_userID, p_tuple.Item2);
                return Created("Order successfully placed!", "");
            }
            catch (System.Exception ex)
            {
                Log.Warning("User had issue placing an order");
                return Conflict(ex.Message);
            }
        }

        [HttpPost("SellOrder")]
        [Authorize(Roles = "0,1")]

        public IActionResult SellOrder(decimal p_amount, string p_CryptoName, int p_userID, SellOrderHistory p_SellOrder)
        {
            try
            {
                Log.Information("User has successfully used Sell Order function");
                return Created("Sell order created", _cryptoBL.SellOrder(p_amount, p_CryptoName, p_userID, p_SellOrder));
            }
            catch (System.Exception ex)
            {
                Log.Warning("User had issue with Sell Order Function");
                return Conflict(ex.Message);
            }
        }

        // PUT: api/User/5
        [HttpPost("AddToWallet")]
        [Authorize(Roles = "0,1")]
        public IActionResult AddtoWallet(decimal p_amount, int p_userID)
        {
            //Possibly have validation for this step
            try
            {
                Log.Information("User has successfully added to wallet");
                return Created("Successfully added to wallet", _cryptoBL.AddtoWallet(p_amount, p_userID));
            }
            catch (System.Exception ex)
            {
                Log.Warning("User had issue adding to wallet");
                return Conflict(ex.Message);
            }
        }

        [HttpGet("ViewWallet")]
        [Authorize(Roles = "0,1")]

        public IActionResult ViewWallet(int p_userID)
        {
            try
            {
                Log.Information("User has successfully viewed wallet");
                return Ok(_cryptoBL.ViewWallet(p_userID));
            }
            catch (SqlException)
            {
                Log.Warning("User had issue viewing wallet");
                return NotFound();
            }
        }

        [HttpGet("ViewAssets")]
        [Authorize(Roles = "0,1")]

        public IActionResult ViewAssets(int p_userID)
        {
            try
            {
                Log.Information("User has successfully viewed assets");
                return Ok(_cryptoBL.ViewAssets(p_userID));
            }
            catch (SqlException)
            {
                Log.Warning("User had issue viewing assets");
                return NotFound();
            }
        }

        [HttpGet("BuyOrderHistory")]
        [Authorize(Roles = "0,1")]

        public IActionResult GetBuyOrderHistoryByCustomer(int p_userID)
        {
            try
            {
                Log.Information("User successfully viewed buy order history");
                return Ok(_cryptoBL.GetBuyOrderHistoryByCustomer(p_userID));
            }
            catch (SqlException)
            {
                Log.Warning("User had issue getting buy order history");
                return NotFound();
            }
        }

        [HttpGet("SellOrderHistory")]
        [Authorize(Roles = "0,1")]

        public IActionResult GetSellOrderHistoryByCustomer(int p_userID)
        {
            try
            {
                Log.Information("User successfully viewed sell order history");
                return Ok(_cryptoBL.GetSellOrderHistoryByCustomer(p_userID));
            }
            catch (SqlException)
            {
                Log.Warning("User had issue getting sell order history");
                return NotFound();
            }
        }

        [HttpPut("UpdateName")]
        [Authorize(Roles = "0,1")]

        public IActionResult UpdateName([FromBody] int p_userID, string p_name)
        {
            try
            {
                Log.Warning("User successfully updated name");
                return Ok(_cryptoBL.UpdateName(p_userID, p_name));
            }
            catch (System.Exception ex)
            {
                Log.Warning("User had issue updating name");
                return Conflict(ex.Message);
            }
        }

        [HttpPut("UpdateUsername")]
        [Authorize(Roles = "0,1")]

        public IActionResult UpdateUsername([FromBody] int p_userID, string p_userName)
        {
            try
            {
                Log.Information("User successfully updated username");
                return Ok(_cryptoBL.UpdateUsername(p_userID, p_userName));
            }
            catch (System.Exception ex)
            {
                Log.Warning("User had issue updating username");
                return Conflict(ex.Message);
            }
        }

        [HttpPut("UpdateAge")]
        [Authorize(Roles = "0,1")]

        public IActionResult UpdateAge([FromBody] int p_userID, int p_age)
        {
            try
            {
                Log.Information("User has successfully updated age");
                return Ok(_cryptoBL.UpdateAge(p_userID, p_age));
            }
            catch (System.Exception ex)
            {
                Log.Warning("User had issue updating age");
                return Conflict(ex.Message);
            }
        }
        
        // DELETE: api/User/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "0,1")]
        public void Delete(int id)
        {
        }
    }
}
