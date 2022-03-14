using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CryptoAPI;
using CryptoBL;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Serilog;

namespace CryptoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IJWTAuthManager _authentication;
        private ICryptoClassBL _cryptoBL;
        public AdminController(ICryptoClassBL p_cryptoBL, IJWTAuthManager authentication)
        {
            _cryptoBL = p_cryptoBL;
            _authentication = authentication;
        }

        // GET: api/Admin
        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "1")]
        public IActionResult GetAllUsers()
        {
            try
            {
                Log.Information("Admin successfully used get all user function");
                return Ok(_cryptoBL.GetAllUsers());
            }
            catch (SqlException)
            {
                Log.Warning("Admin had issue using get all user function");
                return NotFound();
            }
        }

        [HttpPost("AddUser")]
        [Authorize(Roles = "1")]
        public IActionResult AddUser(AccountUser p_NewUser)
        {

            try
            {
                Log.Information("Admin has successfully added user");
                return Ok(_cryptoBL.AddUser(p_NewUser));
            }
            catch (SqlException)
            {
                Log.Warning("Admin had issue adding user");
                return NotFound();
            }
        }

        [HttpGet("ViewWallet")]
        [Authorize(Roles = "1")]

        public IActionResult ViewWallet(int p_userID)
        {
            try
            {
                Log.Information("Admin has successfully viewed wallet");
                return Ok(_cryptoBL.ViewWallet(p_userID));
            }
            catch (SqlException)
            {
                Log.Warning("Admin had issue viewing wallet");
                return NotFound();
            }
        }

        [HttpGet("ViewAssets")]
        [Authorize(Roles = "1")]
        public IActionResult ViewAssets(int p_userID)
        {
            try
            {
                Log.Information("Admin has successfully viewed assets");
                return Ok(_cryptoBL.ViewAssets(p_userID));
            }
            catch (SqlException)
            {
                Log.Warning("Admin had issue viewing assets");
                return NotFound();
            }
        }

        // GET: api/Admin/5
        [HttpGet("SpecificUser")]
        public IActionResult GetSpecificUser(int p_userID)
        {
            try
            {
                Log.Information("Admin successfully got specific user");
                return Ok(_cryptoBL.GetSpecificUser(p_userID));   
            }
            catch (SqlException)
            {
                Log.Warning("Admin had issue retrieving specific user");
                return NotFound();
            }
        }

        [HttpGet("BuyOrderHistory")]
        [Authorize(Roles = "1")]
        public IActionResult GetBuyOrderHistoryByCustomer(int p_userID)
        {
            try
            {
                Log.Information("Admin successfully viewed buy order history");
                return Ok(_cryptoBL.GetBuyOrderHistoryByCustomer(p_userID));
            }
            catch (SqlException)
            {
                Log.Warning("Admin had issue getting buy order history");
                return NotFound();
            }
        }

        // PUT: api/Admin/5
        [HttpPut("BanUser")]
        [Authorize(Roles = "1")]
        public IActionResult BanUser([FromBody] int p_userID)
        {
            try
            {
                Log.Information("Admin successfully banned user");
                return Ok(_cryptoBL.BanUser(p_userID));
            }
            catch (System.Exception ex)
            {
                Log.Warning("Admin had issue banning user");
                return Conflict(ex.Message);
            }
        }

        [HttpGet("SellOrderHistory")]
        [Authorize(Roles = "1")]
        public IActionResult GetSellOrderHistoryByCustomer(int p_userID)
        {
            try
            {
                Log.Information("Admin successfully viewed sell order history");
                return Ok(_cryptoBL.GetSellOrderHistoryByCustomer(p_userID));
            }
            catch (SqlException)
            {
                Log.Warning("Admin had issue getting sell order history");
                return NotFound();
            }
        }

        [HttpPut("UpdateName")]
        [Authorize(Roles = "1")]
        public IActionResult UpdateName([FromBody] int p_userID, string p_name)
        {
            try
            {
                Log.Warning("Admin successfully updated name");
                return Ok(_cryptoBL.UpdateName(p_userID, p_name));
            }
            catch (System.Exception ex)
            {
                Log.Warning("Admin had issue updating name");
                return Conflict(ex.Message);
            }
        }

        [HttpPut("UpdateUsername")]
        [Authorize(Roles = "1")]
        public IActionResult UpdateUsername([FromBody] int p_userID, string p_userName)
        {
            try
            {
                Log.Information("Admin successfully updated username");
                return Ok(_cryptoBL.UpdateUsername(p_userID, p_userName));
            }
            catch (System.Exception ex)
            {
                Log.Warning("Admin had issue updating username");
                return Conflict(ex.Message);
            }
        }

        [HttpPut("UpdateAge")]
        [Authorize(Roles = "1")]
        public IActionResult UpdateAge([FromBody] int p_userID, int p_age)
        {
            try
            {
                Log.Information("Admin has successfully updated age");
                return Ok(_cryptoBL.UpdateAge(p_userID, p_age));
            }
            catch (System.Exception ex)
            {
                Log.Warning("Admin had issue updating age");
                return Conflict(ex.Message);
            }
        }

        // DELETE: api/Admin/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public void Delete(int id)
        {
        }
    }
}
