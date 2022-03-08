using Model;
using CryptoDL;
namespace CryptoBL{
    public class CryptoBL : ICryptoBL
    {
        private IRepository _repo;
        public CryptoBL(IRepository p_repo){
            _repo = p_repo;
        }
        public AddUser(AccountUser p_NewUser)
        {
            return _repo.AddUser(p_NewUser);
        }
        public AddtoWallet(decimal p_amount, int p_userID)
        {
            return _repo.AddtoWallet(p_amount, p_userID);
        }

        public void Notification(decimal p_stopLoss, decimal p_takeProfit)
        {
            // foreach (var item in Asset)
            // {
            //     if(Asset[item].p_stopLoss = /*Need model for current price*/){
                    
            //     }
            // }
        }

        public PlaceOrder(Assets p_NewAsset, decimal p_amount, int p_userID, OrderHistory p_order)
        {
            _repo.SubtractfromWallet(p_amount, p_userID);
            _repo.BuyCrypto(p_NewAsset);
            return _repo.AddOrderHistory(p_order);
        }

        public UserLogin(string p_userName, string p_password)
        {
            return _repo.LoginUser(p_userName, p_password);
        }

        public ViewWallet(int p_userID)
        {
            return _repo.SelectWalletbyCustomer(p_userID);
        }
        public ViewAssets(int p_userID)
        {
            return _repo.GetAssetsbyCustomer(p_userID);
        }
        //GetAllUsers needs to be an admin only feature
        public GetAllUsers()
        {
            return _repo.GetAllUsers();
        }
    }
}