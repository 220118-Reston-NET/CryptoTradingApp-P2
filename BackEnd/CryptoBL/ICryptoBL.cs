namespace CryptoTradingBL{
    public interface ICryptoBL{
        //Add the interface functionalities
        
        //Customer Login Functionality
        /*This will ask the user for a username and password and pass that to the datalayer*/

        //Create New Customer Functionality
        /*This will create a new user in the database with the parameters of:
        username, password, name, age*/

        //View Customer Wallet Functionality
        /*Must be a verified user to access the users wallet*/

        //Set Stop-Loss Order Functionality
        /*
        -A sell-stop order places a limit on a declining price commodity and will sell the commodity if this price is reached.
        -This will be called upon on a place order funtion to create the stop-loss threshold.
        */

        //Set Take-Profit Order Functionality
        /*
        -A take profit order creates a stop point to sell a commodity as it increases in value and sell the commodity if this price is reached.
        -This will be called upon on a place order function to create the take-profit threshold.
        */

        //Send Notification Functionality
        /*
        This functionality will notify the user when a commodity has passed the user set stop-loss or take-profit threshold.
        */

        //Place Order Functionality
        /*
        This functionality will call upon the Stop-Loss function and Take-Profit function to set the values of those fields.
        In addition this function will pass the verified users ID and Name as well as the Current Date and associated price for the commodity.
        */
    }
}