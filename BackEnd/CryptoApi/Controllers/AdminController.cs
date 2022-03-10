using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CryptoBL;
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

        private ICryptoClassBL _cryptoBL;
        public AdminController(ICryptoClassBL p_cryptoBL)
        {
            _cryptoBL = p_cryptoBL;
        }

        // GET: api/Admin
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                Log.Information("Admin successfully used get all user function");
                return Ok(_cryptoBL.GetAllUsers);
            }
            catch (SqlException)
            {
                Log.Warning("Admin had issue using get all user function");
                return NotFound();
            }
        }

        [HttpPost("AddUser")]
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
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Admin
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Admin/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Admin/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
