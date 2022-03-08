using Model;
namespace CryptoDL;
public interface IRepository
{

    AccountUser AddUser(AccountUser _user);

    int LoginUser(string username, string password);

    List<AccountUser> GetAllUsers();

    Wallet SelectWalletbyCustomer(int _userID);

    Wallet AddtoWallet(decimal _amount, int _userID);

    Wallet SubtractfromWallet(decimal amount, int _userID);

    Assets BuyCrypto(Assets _asset);

    List<Assets> GetAssetsbyCustomer(int _userID);

    OrderHistory AddOrderHistory(OrderHistory _orderhis);

    Notification AddNotification(Notification _noti);


}
