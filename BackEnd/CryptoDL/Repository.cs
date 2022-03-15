using System.Data.SqlClient;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Model;
//using System.Web.Script.Serialization;
namespace CryptoDL
{
    public class Repository : IRepository
    {
        private readonly string _connectionStrings;
        public Repository(string c_connectionStrings)
        {
            _connectionStrings = c_connectionStrings;
        }

        public Notification AddNotification(Notification _noti)
        {
            string SQLQuery = @"insert into Notification values(@customerId, @cryptoName, @alertPrice)";
            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);
                command.Parameters.AddWithValue("@customerId", _noti.customerId);
                command.Parameters.AddWithValue("@cryptoName", _noti.cryptoName);
                command.Parameters.AddWithValue("@alertPrice", _noti.alertPrice);

                command.ExecuteNonQuery();
            }
            return _noti;
        }


        public BuyOrderHistory AddBuyOrderHistory(BuyOrderHistory _borderhis)
        {
            string SQLQuery = @"insert into BuyOrderHistory values(@customerId, @cryptoName, @buyPrice, @buyDate, @quantity, @total)";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);
                command.Parameters.AddWithValue("@customerId", _borderhis.customerId);
                command.Parameters.AddWithValue("@cryptoName", _borderhis.cryptoName);
                command.Parameters.AddWithValue("@buyPrice", _borderhis.buyPrice);
                command.Parameters.AddWithValue("@buyDate", _borderhis.buyDate);
                command.Parameters.AddWithValue("@quantity", _borderhis.quantity);
                command.Parameters.AddWithValue("@total", _borderhis.total);

                command.ExecuteNonQuery();
            }
            return _borderhis;
        }

        public SellOrderHistory AddSellOrderHistory(SellOrderHistory _sorderhis)
        {
            string SQLQuery = @"insert into SellOrderHistory values(@customerId, @cryptoName, @sellPrice, @sellDate, @quantity, @total)";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);
                command.Parameters.AddWithValue("@customerId",_sorderhis.customerId);
                command.Parameters.AddWithValue("@cryptoName", _sorderhis.cryptoName);
                command.Parameters.AddWithValue("@sellPrice", _sorderhis.sellPrice);
                command.Parameters.AddWithValue("@sellDate", _sorderhis.sellDate);
                command.Parameters.AddWithValue("@quantity", _sorderhis.quantity);
                command.Parameters.AddWithValue("@total", _sorderhis.total);

                command.ExecuteNonQuery();
            }
            return _sorderhis;
        }

        public Wallet AddtoWallet(decimal _amount, int _userID)
        {
            Wallet _wallet = new Wallet();

            string SQLQuery = @"update Wallet set cash = cash + @amount where customerId = @customerId";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
           {
               con.Open();

               SqlCommand command = new SqlCommand(SQLQuery, con);
               command.Parameters.AddWithValue("@customerId", _userID);
               command.Parameters.AddWithValue("@amount", _amount);

               command.ExecuteNonQuery();
           } 

           _wallet = SelectWalletbyCustomer(_userID);
           return _wallet;

        }

        public AccountUser AddUser(AccountUser _user)
        {
            Wallet _wallet = new Wallet();
            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand("AddUser", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@userName", _user.username);
                command.Parameters.AddWithValue("@userPassword", _user._password);
                command.Parameters.AddWithValue("@name", _user.name);
                command.Parameters.AddWithValue("@age", _user.age);
                command.Parameters.AddWithValue("@dateCreated", _user.dateCreated);

                command.ExecuteNonQuery();
                 
            }
            //wallet initialize part
            List<AccountUser> list = GetAllUsers();
            _user.ID = list[list.Count - 1].ID;
            string SQLQuery = @"insert into Wallet values(@customerId, 10000)";
             using(SqlConnection con = new SqlConnection(_connectionStrings))
           {
               con.Open();

               SqlCommand command = new SqlCommand(SQLQuery, con);
               command.Parameters.AddWithValue("@customerId", _user.ID);

               command.ExecuteNonQuery();
           }

            return _user;
        }

         public Wallet InitializeWallet(int _userId)
        {
            Wallet _wallet = new Wallet();
            string SQLQuery = @"insert into Wallet values(@customerId, 10000)";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
           {
               con.Open();

               SqlCommand command = new SqlCommand(SQLQuery, con);
               command.Parameters.AddWithValue("@customerId", _userId);

               command.ExecuteNonQuery();
           } 

           _wallet = SelectWalletbyCustomer(_userId);
           return _wallet;
        }

        public Assets BuyCrypto(Assets _asset)
        {
            string SQLQuery = @"insert into Assets values(@customerId, @cryptoName, @buyPrice, @buyDate, @stoploss, @takeprofit, @coinQuantity)";
            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);
                command.Parameters.AddWithValue("@customerId", _asset.customerId);
                command.Parameters.AddWithValue("@cryptoName", _asset.cryptoName);
                command.Parameters.AddWithValue("@buyPrice", _asset.buyPrice);
                command.Parameters.AddWithValue("@buyDate", _asset.buyDate);
                command.Parameters.AddWithValue("@stoploss", _asset.stoploss);
                command.Parameters.AddWithValue("@takeprofit", _asset.takeprofit);
                command.Parameters.AddWithValue("@coinQuantity", _asset.coinQuantity);

                command.ExecuteNonQuery();
            }
            return _asset;
        }

        public void DeleteAssetRow(int _userID, string _cryptoName)
        {
            string SQLQuery = @"delete from Assets where customerId = @_userID and cryptoName = @_cryptoName;";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
           {
               con.Open();

               SqlCommand command = new SqlCommand(SQLQuery, con);
               command.Parameters.AddWithValue("@_userID", _userID);
               command.Parameters.AddWithValue("@_cryptoName", _cryptoName);

               command.ExecuteNonQuery();
           } 
        }

        public List<AccountUser> GetAllUsers()
        {
            List<AccountUser> userList = new List<AccountUser>();

            string SQLQuery = @"select * from AccountUser";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    userList.Add(new AccountUser(){
                        ID = reader.GetInt32(0),
                        username = reader.GetString(1),

                       // _password = SHA1HashValue(reader.GetString(2)).ToString(),
                        
                        name = reader.GetString(4),
                        age = reader.GetInt32(5),
                        dateCreated = reader.GetDateTime(6),
                        isBanned = reader.GetInt32(7),
                        isAdmin = reader.GetInt32(8)
                    });
                }
            }

            return userList;
        }

        public List<Assets> GetAssetsbyCustomer(int _userID)
        {
            List<Assets> assetList = new List<Assets>();
            string SQLQuery = @"SELECT * from Assets where customerId = @userID;";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    assetList.Add(new Assets(){
                        customerId = reader.GetInt32(0),
                        cryptoName = reader.GetString(1),
                        buyPrice = reader.GetDecimal(2),
                        buyDate = reader.GetDateTime(3),
                        stoploss = reader.GetDecimal(5),
                        takeprofit = reader.GetDecimal(6),
                        coinQuantity = reader.GetDecimal(7)  
                    });
                }
            }

            return assetList;
        }

        public AccountUser LoginUser(string username, string password)
        {
            AccountUser _user = new AccountUser();
            List<AccountUser> userlist = new List<AccountUser>();
            int id=0;
            int loginResult = 0;
            using(SqlConnection connection = new SqlConnection(_connectionStrings))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UserrLogin", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userName", username);
                command.Parameters.AddWithValue("@userPassword", password);
                //command.Parameters.AddWithValue("@retVal", loginResult);
                
                //SqlDataReader reader = command.ExecuteReader();
                

                loginResult = Convert.ToInt32(command.ExecuteScalar());
            }
            if(loginResult==1)
            {
                userlist = GetAllUsers();
                foreach (AccountUser item in userlist)
                {
                    if(item.username==username)
                    {
                        id=item.ID;

                    }
                }
                if(id!=0)
                {
                    _user=GetSpecificUser(id);
                }

                return _user;
            }
            return _user;
        }

        public Wallet SelectWalletbyCustomer(int _userID)
        {
            Wallet _wallet = new Wallet();
            List<Wallet> walletList = new List<Wallet>();
            string SQLQuery = @"SELECT * from Wallet where customerId = @userID;";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                command.Parameters.AddWithValue("@userID", _userID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    walletList.Add(new Wallet(){
                        customerId = reader.GetInt32(0),
                        cash = reader.GetDecimal(1)
                    });

                }

            }
            _wallet = walletList[0];
            return _wallet;
        }

        public Wallet SubtractfromWallet(decimal amount, int _userID)
        {
            Wallet _wallet = new Wallet();
            string SQLQuery = @"update Wallet set cash = cash - @amount where customerId = @customerId";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
           {
               con.Open();

               SqlCommand command = new SqlCommand(SQLQuery, con);
               command.Parameters.AddWithValue("@customerId", _userID);
               command.Parameters.AddWithValue("@amount", amount);

               command.ExecuteNonQuery();
           } 

           _wallet = SelectWalletbyCustomer(_userID);
           return _wallet;
        }

        public AccountUser BanUser(int _userID)
        {
            AccountUser bannedUser = new AccountUser();
            string SQLQuery = @"update AccountUser set isBanned = 1 where id = @customerId";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
           {
               con.Open();

               SqlCommand command = new SqlCommand(SQLQuery, con);
               command.Parameters.AddWithValue("@customerId", _userID);

               command.ExecuteNonQuery();
           } 

           bannedUser = GetSpecificUser(_userID);
           return bannedUser;
        }

        public AccountUser GetSpecificUser(int _userID)
        {
            List<AccountUser> userList = new List<AccountUser>();

            string SQLQuery = @"select * from AccountUser where id = @ID";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                command.Parameters.AddWithValue("@ID", _userID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    userList.Add(new AccountUser(){
                        ID = reader.GetInt32(0),
                        username = reader.GetString(1),

                        //_password = (byte [])reader.GetValue(2),

                        name = reader.GetString(4),
                        age = reader.GetInt32(5),
                        dateCreated = reader.GetDateTime(6),
                        isBanned = reader.GetInt32(7),
                        isAdmin = reader.GetInt32(8)
                    });
                }
            }

            return userList[0];
        }

       

        public List<BuyOrderHistory> GetBuyOrderHistoryByCustomer(int _userID)
        {
            List<BuyOrderHistory> buyOrderList = new List<BuyOrderHistory>();

            string SQLQuery = @"select * from BuyOrderHistory where customerId = @customerId";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                command.Parameters.AddWithValue("@customerId", _userID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    buyOrderList.Add(new BuyOrderHistory(){
                        customerId = reader.GetInt32(0),
                        cryptoName = reader.GetString(1),
                        buyPrice = reader.GetDecimal(2),
                        buyDate = reader.GetDateTime(3),
                        quantity = reader.GetDecimal(4),
                        total = reader.GetDecimal(5)
                    });
                }
            }
            return buyOrderList;
        }

        public List<SellOrderHistory> GetSellOrderHistoryByCustomer(int _userID)
        {
            List<SellOrderHistory> sellOrderList = new List<SellOrderHistory>();

            string SQLQuery = @"select * from SellOrderHistory where customerId = @customerId";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                command.Parameters.AddWithValue("@customerId", _userID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    sellOrderList.Add(new SellOrderHistory(){
                        customerId = reader.GetInt32(0),
                        cryptoName = reader.GetString(1),
                        sellPrice = reader.GetDecimal(2),
                        sellDate = reader.GetDateTime(3),
                        quantity = reader.GetDecimal(4),
                        total = reader.GetDecimal(5)
                    });
                }
            }
            return sellOrderList;
        }

        public AccountUser UpdateUsername(int _userID, string _username)
        {
            AccountUser _user = new AccountUser();
            string SQLQuery = @"update AccountUser set userName = @username where id = @userID";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
           {
               con.Open();

               SqlCommand command = new SqlCommand(SQLQuery, con);
               command.Parameters.AddWithValue("@userID", _userID);
               command.Parameters.AddWithValue("@username", _username);

               command.ExecuteNonQuery();
           } 
           _user = GetSpecificUser(_userID);
           return _user;
        }

        public AccountUser UpdateName(int _userID, string _name)
        {
            AccountUser _user = new AccountUser();
            string SQLQuery = @"update AccountUser set name = @name where id = @userID";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
           {
               con.Open();

               SqlCommand command = new SqlCommand(SQLQuery, con);
               command.Parameters.AddWithValue("@userID", _userID);
               command.Parameters.AddWithValue("@name", _name);

               command.ExecuteNonQuery();
           } 
           _user = GetSpecificUser(_userID);
           return _user;
        }

        public AccountUser UpdateAge(int _userID, int _age)
        {
            AccountUser _user = new AccountUser();
            string SQLQuery = @"update AccountUser set age = @age where id = @userID";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
           {
               con.Open();

               SqlCommand command = new SqlCommand(SQLQuery, con);
               command.Parameters.AddWithValue("@userID", _userID);
               command.Parameters.AddWithValue("@age", _age);

               command.ExecuteNonQuery();
           } 
           _user = GetSpecificUser(_userID);
           return _user;
        }
        

        public List<CryptoVariables> GetAllPrice()
        {
            List<CryptoVariables> userList = new List<CryptoVariables>();

            string SQLQuery = @"select * from CryptoVariables";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    userList.Add(new CryptoVariables(){
                        cryptoName = reader.GetString(0),
                        currentPrice = reader.GetDecimal(1),
                        alphaVal = reader.GetDecimal(2),
                        betaVal = reader.GetDecimal(3),
                        sandp500Val = reader.GetDecimal(4),
                        randVal = reader.GetDecimal(5),
                        calculated = reader.GetFloat(6)
                    });
                }
            }
            return userList;
        }

        public List<CryptoVariables> GetPredictedPrices()
        {
            List<CryptoVariables> userList = GetAllPrice();

               foreach (CryptoVariables item in userList)
               {
                   item.currentPrice = (item.alphaVal+item.betaVal)*item.sandp500Val*(decimal)item.calculated;

               }
            
            return userList;
        }

        public Assets SetStopLoss(int _userID, decimal _stoploss, string _cryptoName)
        {
             Assets _asset = new Assets();
             List<Assets> assetlist = new List<Assets>();
            string SQLQuery = @"update Assets set stoploss = @stoploss where customerId = @customerId and cryptoName = @cryptoName";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
           {
               con.Open();

               SqlCommand command = new SqlCommand(SQLQuery, con);
               command.Parameters.AddWithValue("@customerId", _userID);
               command.Parameters.AddWithValue("@stoploss", _stoploss);
               command.Parameters.AddWithValue("@cryptoName", _cryptoName);

               command.ExecuteNonQuery();
           } 

           assetlist = GetAssetsbyCustomer(_userID);
           foreach (Assets item in assetlist)
           {
               if(item.cryptoName==_cryptoName)
               {
                   _asset = item;
               }
           }
           return _asset;
        }

        public Assets SetTakeProfit(int _userID, decimal _takeprofit, string _cryptoName)
        {
            Assets _asset = new Assets();
             List<Assets> assetlist = new List<Assets>();
            string SQLQuery = @"update Assets set takeprofit = @takeprofit where customerId = @customerId and cryptoName = @cryptoName";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
           {
               con.Open();

               SqlCommand command = new SqlCommand(SQLQuery, con);
               command.Parameters.AddWithValue("@customerId", _userID);
               command.Parameters.AddWithValue("@takeprofit", _takeprofit);
               command.Parameters.AddWithValue("@cryptoName", _cryptoName);

               command.ExecuteNonQuery();
           } 

           assetlist = GetAssetsbyCustomer(_userID);
           foreach (Assets item in assetlist)
           {
               if(item.cryptoName==_cryptoName)
               {
                   _asset = item;
               }
           }
           return _asset;
        }

        public Assets BuyExistingCrypto(int _userID, decimal _amount, string _cryptoName, DateTime _date)
        {
            Assets _asset = new Assets();
             List<Assets> assetlist = new List<Assets>();
            string SQLQuery = @"update Assets set buyPrice = @buyPrice, buyDate = @buyDate where customerId = @customerId and cryptoName = @cryptoName";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
           {
               con.Open();

               SqlCommand command = new SqlCommand(SQLQuery, con);
               command.Parameters.AddWithValue("@customerId", _userID);
               command.Parameters.AddWithValue("@buyPrice", _amount);
               command.Parameters.AddWithValue("@buyDate", _date);
               command.Parameters.AddWithValue("@cryptoName", _cryptoName);

               command.ExecuteNonQuery();
           } 

           assetlist = GetAssetsbyCustomer(_userID);
           foreach (Assets item in assetlist)
           {
               if(item.cryptoName==_cryptoName)
               {
                   _asset = item;
               }
           }
           return _asset;
        }
    }
}