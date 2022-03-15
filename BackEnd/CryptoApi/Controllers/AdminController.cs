using System.Data.SqlClient;
using CryptoBL;
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
                return Ok(_cryptoBL.GetAllUsers());
            }
            catch (SqlException)
            {
                Log.Warning("Admin had issue using get all user function");
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

        // PUT: api/Admin/5
        [HttpPut("BanUser")]
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
    }
}
