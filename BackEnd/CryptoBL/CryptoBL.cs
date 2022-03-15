using Model;
using CryptoDL;
using System.Data.SqlClient;

namespace CryptoBL{
    public class CryptoClassBL : ICryptoClassBL
    {
        private IRepository _repo;
        public CryptoClassBL(IRepository p_repo){
            _repo = p_repo;
        }
        public AccountUser AddUser(AccountUser p_NewUser)
        {
             _repo.AddUser(p_NewUser);
            return p_NewUser;
        }
        public Wallet AddtoWallet(decimal p_amount, int p_userID)
        {
            return _repo.AddtoWallet(p_amount, p_userID);
        }

        public Notification Notification(int p_userID)
        {
            Notification _setNoti = new Notification(){
                customerId = -1,
                cryptoName = "No Name",
                alertPrice = 0.00m,
            };
            List<CryptoVariables> _futures = CryptoFutures();
            List<Assets> _userasset = ViewAssets(p_userID);
            foreach (var item in _userasset)
            {
                foreach (var item2 in _futures){
                    if (item.stoploss == item2.currentPrice){
                        Notification _notification = new Notification(){
                            cryptoName = item2.cryptoName,
                            alertPrice = item2.currentPrice,
                        };
                        return _notification;   
                    }
                    else if (item.takeprofit == item2.currentPrice){
                        Notification _notification = new Notification(){
                            cryptoName = item2.cryptoName,
                            alertPrice = item2.currentPrice,
                        };
                        return _notification; 
                    }
                    else{
                        return _setNoti;
                    }
                }
            }
            return _setNoti;
        }

        public BuyOrderHistory PlaceOrder(Assets p_NewAsset, decimal p_amount, int p_userID, BuyOrderHistory p_order)
        {
            _repo.SubtractfromWallet(p_amount, p_userID);
            _repo.BuyCrypto(p_NewAsset);
            return _repo.AddBuyOrderHistory(p_order);
        }
        public SellOrderHistory SellOrder(decimal p_amount, string p_CryptoName, int p_userID, SellOrderHistory p_SellOrder){
            _repo.DeleteAssetRow(p_userID, p_CryptoName);
            _repo.AddtoWallet(p_amount, p_userID);
            return _repo.AddSellOrderHistory(p_SellOrder);
        }

        public AccountUser UserLogin(string p_userName, string p_password)
        {
            return _repo.LoginUser(p_userName, p_password);
        }

        public Wallet ViewWallet(int p_userID)
        {
            return _repo.SelectWalletbyCustomer(p_userID);
        }
        public List<Assets> ViewAssets(int p_userID)
        {
            return _repo.GetAssetsbyCustomer(p_userID);
        }
        //GetAllUsers needs to be an admin only feature
        public List<AccountUser> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }
        public AccountUser GetSpecificUser(int p_userID)
        {
            return _repo.GetSpecificUser(p_userID);
        }

        public AccountUser BanUser(int p_userID)
        {
            return _repo.BanUser(p_userID);
        }

        public List<BuyOrderHistory> GetBuyOrderHistoryByCustomer(int p_userID)
        {
            return _repo.GetBuyOrderHistoryByCustomer(p_userID);
        }

        public List<SellOrderHistory> GetSellOrderHistoryByCustomer(int p_userID)
        {
            return _repo.GetSellOrderHistoryByCustomer(p_userID);
        }

        public AccountUser UpdateUsername(int p_userID, string p_userName)
        {
            return _repo.UpdateUsername(p_userID, p_userName);
        }

        public AccountUser UpdateName(int p_userID, string p_name)
        {
            return _repo.UpdateName(p_userID, p_name);
        }

        public AccountUser UpdateAge(int p_userID, int p_age)
        {
            return _repo.UpdateAge(p_userID, p_age);
        }

        public List<CryptoVariables> CryptoFutures()
        {
            return _repo.GetPredictedPrices();
        }
    }
}
