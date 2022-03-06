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
            string SQLQuery = @"insert into AccountUser values(@username, @name, @age, @dateCreated, @isBanned, @isAdmin)";

            using(SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(SQLQuery, con);
                command.Parameters.AddWithValue("@username", _user.username);
                command.Parameters.AddWithValue("@name", _user.name);
                command.Parameters.AddWithValue("@age", _user.age);
                command.Parameters.AddWithValue("@dateCreated", _user.dateCreated);
                command.Parameters.AddWithValue("@isBanned", _user.isBanned);
                command.Parameters.AddWithValue("@isAdmin", _user.isAdmin);

                command.ExecuteNonQuery();
            }
            return _user;
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
                        name = reader.GetString(2),
                        age = reader.GetInt32(3),
                        dateCreated = reader.GetDateTime(4),
                        isBanned = reader.GetInt32(5),
                        isAdmin = reader.GetInt32(6)
                    });
                }
            }

            return userList;
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