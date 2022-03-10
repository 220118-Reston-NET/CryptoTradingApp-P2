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

        // GET: api/User
        [HttpPost("AddUser")]
        public IActionResult AddUser(AccountUser p_NewUser)
        {

            try
            {
                Log.Information("Added user successfully");
                return Ok(_cryptoBL.AddUser(p_NewUser));
            }
            catch (SqlException)
            {
                Log.Warning("Issue with add user function");
                return NotFound();
            }
        }

        // GET: api/User/5
        [HttpPost("PlaceOrder")]
        public IActionResult PlaceOrder(Assets p_NewAsset, decimal p_amount, int p_userID, BuyOrderHistory p_order)
        {
            try
            {
                Log.Information("User has placed order successfully"); 
                _cryptoBL.PlaceOrder(p_NewAsset, p_amount, p_userID, p_order);
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

        // POST: api/User
        [HttpPost("Login")]
        public IActionResult UserLogin([FromBody] string p_userName, string p_password)
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

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
