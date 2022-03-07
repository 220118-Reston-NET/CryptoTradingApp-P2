using Model;
using CryptoDL;
namespace CryptoBL{
    public class CryptoBL : ICryptoBL
    {
        private IRepository _repo;
        public CryptoBL(IRepository p_repo){
            _repo = p_repo;
        }
        //Start Adding Buisness layer funcitonalities
        public void NewUser(AccountUser p_NewUser)
        {
            return _repo.NewUser(p_NewUser);
        }

        public void Notification(decimal p_stopLoss, decimal p_takeProfit)
        {
            foreach (var item in Asset)
            {
                if(Asset[item].p_stopLoss = /*Need model for current price*/){
                    
                }
            }
        }

        public void PlaceOrder(Asset p_NewAsset)
        {
            return _repo.PlaceOrder(p_NewAsset);
        }

        public void UserLogin(string p_userName, string p_password)
        {
            return _repo.UserLogin(p_userName, p_password);
        }

        public void ViewWallet()
        {
            return _repo.ViewWallet();
        }
    }
}