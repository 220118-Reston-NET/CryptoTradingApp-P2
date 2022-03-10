using System.Data.SqlClient;
using Model;
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
            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand("AddUser", con);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@username", _user.username);
                command.Parameters.AddWithValue("@password", _user._password);
                command.Parameters.AddWithValue("@name", _user.name);
                command.Parameters.AddWithValue("@age", _user.age);
                command.Parameters.AddWithValue("@dateCreated", _user.dateCreated);

                command.ExecuteNonQuery();
            }
            return _user;
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
                        _password = (byte [])reader.GetValue(2),
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

        public int LoginUser(string username, string password)
        {
            int loginResult = 0;
            using(SqlConnection connection = new SqlConnection(_connectionStrings))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UserLogin", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                
                loginResult = Convert.ToInt32(command.ExecuteScalar());
            }
            return loginResult;
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
                        _password = (byte [])reader.GetValue(2),
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

        public Wallet InitializeWallet(int _userId)
        {
            Wallet _wallet = new Wallet();
            string SQLQuery = @"insert into Wallet values(@customerId, 0)";

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
    }
}