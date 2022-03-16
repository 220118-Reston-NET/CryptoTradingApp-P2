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

        // Get: api/User
        [HttpGet("Login")]
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
        public IActionResult PlaceOrder(int p_userID, decimal p_amount, string _cryptoName, decimal _cryptoprice)
        {
            decimal _coinQuantity = Math.Round(p_amount/_cryptoprice, 4);
            Assets _newAsset = new Assets()
            {
                customerId = p_userID,
                cryptoName = _cryptoName,
                buyPrice = p_amount,
                buyDate = DateTime.Now,
                stoploss = 0,
                takeprofit = 0,
                coinQuantity = _coinQuantity
            };
            BuyOrderHistory _newbhis = new BuyOrderHistory()
            {
                customerId = p_userID,
                cryptoName = _cryptoName,
                buyPrice = _cryptoprice,
                buyDate = DateTime.Now,
                quantity = _coinQuantity,
                total = p_amount
            };



            try
            {
                Log.Information("User has placed order successfully"); 
                if ( _cryptoBL.PlaceOrder(_newAsset, _newbhis, _cryptoprice, p_amount, p_userID, _cryptoName) == null)
                {
                     return Conflict("Insufficient funds");
                }
                return Created("Order successfully placed!", "");
            }
            catch (SqlException ex)
            {
                Log.Warning("User had issue placing an order");
                return Conflict(ex.Message);
            }
        }

        [HttpPost("SellOrder")]
        public IActionResult SellOrder(decimal p_amount, string p_CryptoName, int p_userID, decimal p_cryptoPrice)
        {
            decimal _quantity = p_amount/p_cryptoPrice;
            SellOrderHistory _newHistory = new SellOrderHistory()
            {
                customerId = p_userID,
                cryptoName = p_CryptoName,
                sellPrice = p_cryptoPrice,
                sellDate = DateTime.Now,
                quantity = _quantity,
                total = p_amount
            };
            try
            {
                Log.Information("User has successfully used Sell Order function");
                return Created("Sell order created", _cryptoBL.SellOrder(p_amount, p_CryptoName, p_userID, _newHistory, p_cryptoPrice));
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
        public IActionResult UpdateName(int p_userID, string p_name)
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
        public IActionResult UpdateUsername(int p_userID, string p_userName)
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

        public IActionResult UpdateAge(int p_userID, int p_age)
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

        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword(string p_userName, string p_password)
        {
            try
            {
                Log.Information("User successfull updated password");
                return Ok(_cryptoBL.UpdatePassword(p_userName, p_password));
            }
            catch (System.Exception ex)
            {
                Log.Warning("User had issue updating password");
                return Conflict(ex.Message);
            }
           
        }

        [HttpPut("UpdateProfit")]

        public IActionResult UpdateTakeProfit(int p_userID, decimal p_amount, string p_cryptoName)
        {
            try
            {
                Log.Information("User has successfully updated take profit");
                return Ok(_cryptoBL.UpdateTakeProfit(p_userID, p_amount, p_cryptoName));
            }
            catch (System.Exception ex)
            {
                Log.Warning("User had issue updating take profit");
                return Conflict(ex.Message);
            }
        }

        [HttpPut("UpdateStopLoss")]

        public IActionResult UpdateStopLoss(int p_userID, decimal p_amount, string p_cryptoName)
        {
            try
            {
                Log.Information("User has successfully updated stop loss");
                return Ok(_cryptoBL.UpdateStopLoss(p_userID, p_amount, p_cryptoName));
            }
            catch (System.Exception ex)
            {
                Log.Warning("User had issue updating stop loss");
                return Conflict(ex.Message);
            }
        }
        
        // DELETE: api/User/5
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int p_userID)
        {
            try
            {
                Log.Information("Successfully deleted user");
                _cryptoBL.DeleteUser(p_userID);
                return Ok("You have successfully deleted user");
            }
            catch (System.Exception ex)
            {
                Log.Warning("User had issue using delete function");
                return Conflict(ex.Message);
            }
        }
    }
}
