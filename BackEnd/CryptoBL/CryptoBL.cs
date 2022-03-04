namespace CryptoTradingBL{
    public class CryptoBL : ICryptoBL
    {
        private IRepository _repo;
        public CryptoBL(IRepository p_repo){
            _repo = p_repo;
        }
        //Start Adding Buisness layer funcitonalities
        public void NewUser(string p_userName, string p_name, int p_age, byte p_password)
        {
            throw new NotImplementedException();
        }

        public void Notification(decimal p_alertPrice)
        {
            throw new NotImplementedException();
        }

        public void PlaceOrder(string p_cryptoName, decimal p_buyPrice)
        {
            throw new NotImplementedException();
        }

        public void StopLossOrder(decimal p_stopLoss)
        {
            throw new NotImplementedException();
        }

        public void TakeProfitOrder(decimal p_takeProfit)
        {
            throw new NotImplementedException();
        }

        public void UserLogin(string p_userName, string p_password)
        {
            throw new NotImplementedException();
        }

        public void ViewWallet()
        {
            throw new NotImplementedException();
        }
    }
}