namespace CryptoTradingBL{
    public interface ICryptoBL{
        //Add the interface functionalities
        
        //Customer Login Functionality
        /*This will ask the user for a username and password and pass that to the datalayer*/
        public void UserLogin(string p_userName, string p_password);
        //Create New Customer Functionality
        /*This will create a new user in the database with the parameters of:
        username, password, name, age*/
        public void NewUser(string p_userName, string p_name, int p_age, byte p_password);
        //View Customer Wallet Functionality
        /*Must be a verified user to access the users wallet*/
        public void ViewWallet();
        //Set Stop-Loss Order Functionality
        /*
        -A sell-stop order places a limit on a declining price commodity and will sell the commodity if this price is reached.
        -This will be called upon on a place order funtion to create the stop-loss threshold.
        */
        public void StopLossOrder(decimal p_stopLoss);
        //Set Take-Profit Order Functionality
        /*
        -A take profit order creates a stop point to sell a commodity as it increases in value and sell the commodity if this price is reached.
        -This will be called upon on a place order function to create the take-profit threshold.
        */
        public void TakeProfitOrder(decimal p_takeProfit);
        //Send Notification Functionality
        /*
        This functionality will notify the user when a commodity has passed the user set stop-loss or take-profit threshold.
        */
        public void Notification(decimal p_alertPrice);
        //Place Order Functionality
        /*
        This functionality will call upon the Stop-Loss function and Take-Profit function to set the values of those fields.
        In addition this function will pass the verified users ID and Name as well as the Current Date and associated price for the commodity.
        */
        public void PlaceOrder(string p_cryptoName, decimal p_buyPrice);
    }
}