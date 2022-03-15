using Model;
namespace CryptoDL;
public interface IRepository
{

    AccountUser AddUser(AccountUser _user);

    AccountUser LoginUser(string username, string password);

    Wallet InitializeWallet(int _userId);

    List<AccountUser> GetAllUsers();

    AccountUser GetSpecificUser(int _userID);

    Wallet SelectWalletbyCustomer(int _userID);

    Wallet AddtoWallet(decimal _amount, int _userID);

    Wallet SubtractfromWallet(decimal amount, int _userID);

    AccountUser BanUser(int _userID);

    Assets BuyCrypto(Assets _asset);

    Assets SetStopLoss(int _userID, decimal _stoploss, string _cryptoName);

    Assets SetTakeProfit(int _userID, decimal _takeprofit, string _cryptoName);

    Assets BuyExistingCrypto(int _userID, decimal _amount, string _cryptoName, DateTime _date, decimal _cryptoQuantity);

    List<Assets> GetAssetsbyCustomer(int _userID);

    void DeleteAssetRow(int _userID, string _cryptoName);
    BuyOrderHistory AddBuyOrderHistory(BuyOrderHistory _borderhis);

    List<BuyOrderHistory> GetBuyOrderHistoryByCustomer(int _userID);

    SellOrderHistory AddSellOrderHistory(SellOrderHistory _sorderhis);

    List<SellOrderHistory> GetSellOrderHistoryByCustomer(int _userID);

    Notification AddNotification(Notification _noti);

    AccountUser UpdateUsername(int _userID, string _username);

    AccountUser UpdateName(int _userID, string _name);

    AccountUser UpdateAge(int _userID, int _age);

    List<CryptoVariables> GetAllPrice();
    List<CryptoVariables> GetPredictedPrices();



}
