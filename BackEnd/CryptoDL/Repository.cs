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

        public OrderHistory AddOrderHistory(OrderHistory _orderhis)
        {
            string SQLQuery = @"insert into OrderHistory values(@customerId, @cryptoName, @buyPrice, @buyDate, @sellPrice, @sellDate, @totalReturn)";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);
                command.Parameters.AddWithValue("@customerId", _orderhis.customerId);
                command.Parameters.AddWithValue("@cryptoName", _orderhis.cryptoName);
                command.Parameters.AddWithValue("@buyPrice", _orderhis.buyPrice);
                command.Parameters.AddWithValue("@buyDate", _orderhis.buyDate);
                command.Parameters.AddWithValue("@sellPrice", _orderhis.sellPrice);
                command.Parameters.AddWithValue("@sellDate", _orderhis.sellDate);
                command.Parameters.AddWithValue("@totalReturn", _orderhis.totalReturn);

                command.ExecuteNonQuery();
            }
            return _orderhis;
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
                command.Parameters.AddWithValue("coinQuantity", _asset.coinQuantity);

                command.ExecuteNonQuery();
            }
            return _asset;
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
    }
}