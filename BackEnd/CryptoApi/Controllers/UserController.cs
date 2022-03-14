using Microsoft.AspNetCore.Mvc;
using CryptoBL;
using System.Data.SqlClient;
using Model;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Dapper;
using System.Data;

namespace CryptoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ICryptoClassBL _cryptoBL;
        public UserController(ICryptoClassBL p_cryptoBL)
        {
            _cryptoBL = p_cryptoBL;
        }

        // POST: api/User
        [HttpPost("Login")]
        public IActionResult UserLogin(string p_userName, string p_password)
        {
            //Need Validation for if incorrect username is put in 
    
            try
            {
                Log.Information("User has logged in successfully");
                return Ok(_cryptoBL.UserLogin(p_userName, p_password));
            }
            catch (System.Exception)
            {
                Log.Warning("User had issue logging in");
                return NotFound();
            }
        }

        // GET: api/User
        [HttpPost("AddUser")]
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
        public void Delete(int id)
        {
        }
    }
}
