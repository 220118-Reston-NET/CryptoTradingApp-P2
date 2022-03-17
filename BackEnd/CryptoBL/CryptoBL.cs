using Model;
using CryptoDL;
using System.Data.SqlClient;
using System.Text.Json;

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
            string _filename = "A:/Project2/Remove Authentication/CryptoTradingApp-P2/BackEnd/stockprices.json";
            string jsonString = File.ReadAllText(_filename);
            Notification _setNoti = JsonSerializer.Deserialize<Notification>(jsonString);
            _setNoti.customerId = p_userID;
            List<CryptoVariables> _futures = CryptoFutures();
            List<Assets> _userasset = ViewAssets(p_userID);
            foreach (var item in _userasset)
            {
                foreach (var item2 in _futures){
                    if (item.stoploss == item2.currentPrice){
                        _setNoti.cryptoName = item2.cryptoName;
                        _setNoti.alertPrice = item2.currentPrice;
                        return _setNoti;   
                    }
                    else if (item.takeprofit == item2.currentPrice){
                        _setNoti.cryptoName = item2.cryptoName;
                        _setNoti.alertPrice = item2.currentPrice;
                        return _setNoti;
                    }
                    else{
                        return _setNoti;
                    }
                }
            }
            return _setNoti;
        }

        public BuyOrderHistory PlaceOrder(Assets p_NewAsset, BuyOrderHistory p_order, decimal p_cryptoPrice, decimal p_amount, int p_userID, string p_cryptoName)
        {
            try{
                foreach (var item in ViewAssets(p_userID))
                {
                    if(item.cryptoName == p_cryptoName){
                        _repo.SubtractfromWallet(p_amount, p_userID);
                        _repo.BuyExistingCrypto(p_userID, p_amount, p_cryptoName, DateTime.Now, p_NewAsset.coinQuantity);
                        return _repo.AddBuyOrderHistory(p_order);
                    }
                }
                _repo.SubtractfromWallet(p_amount, p_userID);
                _repo.BuyCrypto(p_NewAsset);
                return _repo.AddBuyOrderHistory(p_order);
            }
            catch(SqlException){
                return null;
            }
        }
        public SellOrderHistory SellOrder(decimal p_amount, string p_CryptoName, int p_userID, SellOrderHistory p_SellOrder, decimal p_cryptoPrice){
            List<Assets> _assets = _repo.GetAssetsbyCustomer(p_userID);
            foreach (var item in _assets)
            {
                if(item.coinQuantity != 0){
                    _repo.DeleteAssetRow(p_userID, p_CryptoName);
                    _repo.AddtoWallet(p_amount, p_userID);
                    return _repo.AddSellOrderHistory(p_SellOrder);
                }
            }
            return null;
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

        public Assets UpdateTakeProfit(int p_userID, decimal p_amount, string p_cryptoName)
        {
            return _repo.SetTakeProfit(p_userID, p_amount, p_cryptoName);
        }

        public Assets UpdateStopLoss(int p_userID, decimal p_amount, string p_cryptoName)
        {
            return _repo.SetStopLoss(p_userID, p_amount, p_cryptoName);
        }
        public void DeleteUser(int p_userID){
            _repo.DeleteUser(p_userID);
        }

        public AccountUser UpdatePassword(string p_userName, string p_password)
        {
            return _repo.UpdatePassword(p_userName, p_password);
        }
    }
}
