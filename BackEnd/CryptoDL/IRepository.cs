using Model;
namespace CryptoDL;
public interface IRepository
{

    AccountUser AddUser(AccountUser _user);

    int LoginUser(string username, string password);

    Wallet InitializeWallet(int _userId);

    List<AccountUser> GetAllUsers();

    AccountUser GetSpecificUser(int _userID);

    Wallet SelectWalletbyCustomer(int _userID);

    Wallet AddtoWallet(decimal _amount, int _userID);

    Wallet SubtractfromWallet(decimal amount, int _userID);

    AccountUser BanUser(int _userID);

    Assets BuyCrypto(Assets _asset);

    List<Assets> GetAssetsbyCustomer(int _userID);

    void DeleteAssetRow(int _userID, string _cryptoName);

    BuyOrderHistory AddBuyOrderHistory(BuyOrderHistory _borderhis);
    SellOrderHistory AddSellOrderHistory(SellOrderHistory _sorderhis);

    Notification AddNotification(Notification _noti);


}
