using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CryptoBL;
using System.Data.SqlClient;
using Model;

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
                //Need to add logging here
                return Ok(_cryptoBL.AddUser(p_NewUser));
            }
            catch (SqlException)
            {
                //Add logging 
                return NotFound();
            }
        }

        // GET: api/User/5
        [HttpPost("PlaceOrder")]
        public IActionResult PlaceOrder(Assets p_NewAsset, decimal p_amount, int p_userID, BuyOrderHistory p_order)
        {
            try
            {
                //Add logging 
                _cryptoBL.PlaceOrder(p_NewAsset, p_amount, p_userID, p_order);
                return Created("Order successfully placed!", "");
            }
            catch (System.Exception ex)
            {
                //Add logging
                return Conflict(ex.Message);
            }
        }

        [HttpPost("SellOrder")]

        public IActionResult SellOrder(decimal p_amount, string p_CryptoName, int p_userID, SellOrderHistory p_SellOrder)
        {
            try
            {
                return Created("Sell order created", _cryptoBL.SellOrder(p_amount, p_CryptoName, p_userID, p_SellOrder));
            }
            catch (System.Exception ex)
            {
                
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
                //Add logging
                return Ok(_cryptoBL.UserLogin(p_userName, p_password));
            }
            catch (System.Exception)
            {
                //Add logging
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
                //Add logging
                return Created("Successfully added to wallet", _cryptoBL.AddtoWallet(p_amount, p_userID));
            }
            catch (System.Exception ex)
            {
                //Add logging
                return Conflict(ex.Message);
            }
        }

        [HttpGet("ViewWallet")]

        public IActionResult ViewWallet(int p_userID)
        {
            try
            {
                //Add logging
                return Ok(_cryptoBL.ViewWallet(p_userID));
            }
            catch (SqlException)
            {
                //Add logging
                return NotFound();
            }
        }

        [HttpGet("ViewAssets")]

        public IActionResult ViewAssets(int p_userID)
        {
            try
            {
                //Add logging
                return Ok(_cryptoBL.ViewAssets(p_userID));
            }
            catch (SqlException)
            {
                //Add logging
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
