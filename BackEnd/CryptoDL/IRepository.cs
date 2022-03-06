using Model;
namespace CryptoDL;
public interface IRepository
{

    AccountUser AddUser(AccountUser _user);

    List<AccountUser> GetAllUsers();

    Wallet SelectWalletbyCustomer(int _userID);

    Wallet AddtoWallet(decimal _amount, int _userID);

    Wallet SubtractfromWallet(decimal amount, int _userID);


}
