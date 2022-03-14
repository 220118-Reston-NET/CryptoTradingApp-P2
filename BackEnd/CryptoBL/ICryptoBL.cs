using Model;
namespace CryptoBL{
    public interface ICryptoClassBL{
        
        public AccountUser UserLogin(string p_userName, string p_password);       
        public AccountUser AddUser(AccountUser p_NewUser);
        public AccountUser GetSpecificUser(int p_userID);
        public AccountUser BanUser(int p_userID);
        public Wallet AddtoWallet(decimal p_ammount, int p_userID);
        public Wallet ViewWallet(int p_userID);
        //Send Notification Functionality
        /*
        This functionality will notify the user when a commodity has passed the user set stop-loss or take-profit threshold.
        */
        public Notification Notification(int p_userID);
        public BuyOrderHistory PlaceOrder(Assets p_NewAsset, decimal p_amount, int p_userID, BuyOrderHistory p_order);
        public SellOrderHistory SellOrder(decimal p_amount, string p_CryptoName, int p_userID, SellOrderHistory p_SellOrder);
        public List<Assets> ViewAssets(int p_userID);
        public List<AccountUser> GetAllUsers();
        public List<BuyOrderHistory> GetBuyOrderHistoryByCustomer (int p_userID);
        public List<SellOrderHistory> GetSellOrderHistoryByCustomer (int p_userID);
        public AccountUser UpdateUsername (int p_userID, string p_userName);
        public AccountUser UpdateName (int p_userID, string p_name);
        public AccountUser UpdateAge (int p_userID, int p_age);
        public List<CryptoVariables> CryptoFutures ();
    }
}