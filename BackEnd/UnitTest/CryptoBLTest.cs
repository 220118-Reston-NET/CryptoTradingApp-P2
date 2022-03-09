using System;
using System.Collections.Generic;
using Model;
using CryptoDL;
using CryptoBL;
using Moq;
using Xunit;



namespace UnitTest;

public class CryptoBLTest{
    [Fact]
    public void AddUserValueTest(){
        int _validID = 1;
        string _validUserName = "Username";
        string _validName = "Name";
        int _validAge = 18;
        DateTime _validDateTime = new DateTime(2022,3,8);
        int _validIsBanned = 0;
        int _validIsAdmin = 1;
        AccountUser _testUser = new AccountUser(){
            ID = _validID,
            username = _validUserName,
            name = _validName,
            age = _validAge,
            dateCreated = _validDateTime,
            isBanned = _validIsBanned,
            isAdmin = _validIsAdmin,
        };

        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.AddUser(_testUser)).Returns(_testUser);

        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        AccountUser actualUser = cryptoBL.AddUser(_testUser);

        Assert.Same(_testUser, actualUser);
    }
    [Fact]
    public void AddToWalletValueTest(){
        int _validCustomerID = 1;
        decimal _validCash = 1.00m;
        Wallet _testWallet = new Wallet(){
            customerId = _validCustomerID,
            cash = _validCash,
        };
        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.AddtoWallet(1.00m, 1)).Returns(_testWallet);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        Wallet actualWallet = cryptoBL.AddtoWallet(1.00m, 1);

        Assert.Same(_testWallet, actualWallet);
    }
    [Fact]
    public void NotificationValueTest(){

    }
    [Fact]
    public void PlaceOrderValueTest(){
        int _validCustomerID = 1;
        string _validCryptoName = "Crypto";
        decimal _validBuyPrice = 1.00m;
        DateTime _validBuyDate = new DateTime(2022,3,8);
        decimal _validQuantity = 1.00m;
        decimal _validTotal = 1.00m;
        decimal _validStopLoss = 1.00m;
        decimal _validTakeProfit = 1.00m;
        decimal _validCoinQuantity = 1.00m;
        decimal _validCash = 1.00m;
        BuyOrderHistory _testBuyOrder = new BuyOrderHistory(){
            customerId = _validCustomerID,
            cryptoName = _validCryptoName,
            buyPrice = _validBuyPrice,
            buyDate = _validBuyDate,
            quantity = _validQuantity,
            total = _validTotal,
        };
        Assets _testAssets = new Assets(){
            customerId = _validCustomerID,
            cryptoName = _validCryptoName,
            buyPrice = _validBuyPrice,
            buyDate = _validBuyDate,
            stoploss = _validStopLoss,
            takeprofit = _validTakeProfit,
            coinQuantity = _validCoinQuantity,
        };
        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.AddBuyOrderHistory(_testBuyOrder)).Returns(_testBuyOrder);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        BuyOrderHistory _actualBuyOrder = cryptoBL.PlaceOrder(_testAssets, _validCash, _validCustomerID, _testBuyOrder);

        Assert.Same(_testBuyOrder, _actualBuyOrder);
    }
    [Fact]
    public void UserLoginValueTest(){
        string _validUserName = "Username";
        string _validPassword = "password";
        int _validLoginResult = 1;

        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.LoginUser(_validUserName, _validPassword)).Returns(_validLoginResult);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        int _loginResult = cryptoBL.UserLogin(_validUserName, _validPassword);

        Assert.Equal(_validLoginResult, _loginResult);
    }
    [Fact]
    public void ViewWalletValueTest(){
        int _validCustomerID = 1;
        decimal _validCash = 1.00m;
        Wallet _testWallet = new Wallet(){
            customerId = _validCustomerID,
            cash = _validCash,
        };
        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.SelectWalletbyCustomer(_validCustomerID)).Returns(_testWallet);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        Wallet _actualWallet = cryptoBL.ViewWallet(_validCustomerID);

        Assert.Same(_testWallet, _actualWallet);
    }
    [Fact]
    public void GetAllUSersValueTest(){
        int _validID = 1;
        string _validUserName = "Username";
        string _validName = "Name";
        int _validAge = 1;
        DateTime _validDateCreated = new DateTime(2022, 3, 8);
        int _validIsBanned = 0;
        int _validIsAdmin = 0;
        AccountUser _testUser = new AccountUser(){
            ID = _validID,
            username = _validUserName,
            name = _validName,
            age = _validAge,
            dateCreated = _validDateCreated,
            isBanned = _validIsBanned,
            isAdmin = _validIsAdmin,
        };
        List<AccountUser> _expectedUser = new List<AccountUser>();
        _expectedUser.Add(_testUser);
        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.GetAllUsers()).Returns(_expectedUser);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        List<AccountUser> _actualUser = cryptoBL.GetAllUsers();

        Assert.Same(_expectedUser, _actualUser);
    }
    [Fact]
    public void SellOrderValueTest(){
        decimal _validAmount = 1.00m;
        string _validCryptoName = "Crypto";
        int _validID = 1;
        decimal _validSellPrice = 1.00m;
        DateTime _validSaleDate = new DateTime(2022, 3, 9);
        decimal _validQuantity = 1.00m;
        decimal _validTotal = 1.00m;
        SellOrderHistory _validSellOrder = new SellOrderHistory(){
            customerId = _validID,
            cryptoName = _validCryptoName,
            sellPrice = _validSellPrice,
            sellDate = _validSaleDate,
            quantity = _validQuantity,
            total = _validTotal,
        };
        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.AddSellOrderHistory(_validSellOrder)).Returns(_validSellOrder);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        SellOrderHistory _actualSellOrder = cryptoBL.SellOrder(_validAmount, _validCryptoName, _validID, _validSellOrder);

        Assert.Same(_validSellOrder, _actualSellOrder);
    }
}