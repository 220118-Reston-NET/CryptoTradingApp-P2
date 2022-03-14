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
        decimal _validAmount = 1.00m;
        Wallet _testWallet = new Wallet(){
            customerId = _validID,
            cash = _validAmount,
        };
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
        mockRepo.Setup(repo => repo.InitializeWallet(_validID)).Returns(_testWallet);

        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        AccountUser _actualUser = cryptoBL.AddUser(_testUser);

        Assert.Same(_testUser, _actualUser);
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
    [Fact]
    public void GetSpecificUserValueTest(){
        int _validID = 1;
        AccountUser _testUser = new AccountUser(){
            ID = _validID,
        };
        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.GetSpecificUser(_validID)).Returns(_testUser);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        AccountUser _actualUser = cryptoBL.GetSpecificUser(_validID);

        Assert.Same(_testUser, _actualUser);
    }
    [Fact]
    public void BanUserValueTest(){
        int _validID = 1;
        AccountUser _testUser = new AccountUser(){
            ID = _validID,
        };
        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.BanUser(_validID)).Returns(_testUser);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        AccountUser _actualUser = cryptoBL.BanUser(_validID);

        Assert.Same(_testUser, _actualUser);
    }
    [Fact]
    public void GetBuyOrderHistoryByCustomerValueTest(){
        int _validCustomerID = 1;
        string _validCryptoName = "Crypto";
        decimal _validBuyPrice = 1.00m;
        DateTime _validBuyDate = new DateTime(2022, 3, 10);
        decimal _validQuantity = 1.00m;
        decimal _validTotal = 1.00m;
        BuyOrderHistory _testOrderHistory = new BuyOrderHistory(){
            customerId = _validCustomerID,
            cryptoName = _validCryptoName,
            buyPrice = _validBuyPrice,
            buyDate = _validBuyDate,
            quantity = _validQuantity,
            total = _validTotal,
        };
        List<BuyOrderHistory> _expectedList = new List<BuyOrderHistory>();
        _expectedList.Add(_testOrderHistory);
        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.GetBuyOrderHistoryByCustomer(_validCustomerID)).Returns(_expectedList);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        List<BuyOrderHistory> _actualList = cryptoBL.GetBuyOrderHistoryByCustomer(_validCustomerID);

        Assert.Same(_expectedList, _actualList);
    }
    [Fact]
    public void GetSellOrderHistoryByCustomerValueTest(){
        int _validCustomerID = 1;
        string _validCryptoName = "Crypto";
        decimal _validBuyPrice = 1.00m;
        DateTime _validBuyDate = new DateTime(2022, 3, 10);
        decimal _validQuantity = 1.00m;
        decimal _validTotal = 1.00m;
        SellOrderHistory _testOrderHistory = new SellOrderHistory(){
            customerId = _validCustomerID,
            cryptoName = _validCryptoName,
            sellPrice = _validBuyPrice,
            sellDate = _validBuyDate,
            quantity = _validQuantity,
            total = _validTotal,
        };
        List<SellOrderHistory> _expectedList = new List<SellOrderHistory>();
        _expectedList.Add(_testOrderHistory);
        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.GetSellOrderHistoryByCustomer(_validCustomerID)).Returns(_expectedList);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        List<SellOrderHistory> _actualList = cryptoBL.GetSellOrderHistoryByCustomer(_validCustomerID);

        Assert.Same(_expectedList, _actualList);
    }
    [Fact]
    public void UpdateUsernameValueTest(){
        int _validCustomerID = 1;
        string _validNewUsername = "New Username";
        AccountUser _testUser = new AccountUser(){
            ID = _validCustomerID,
            username = _validNewUsername,
        };
        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.UpdateUsername(_validCustomerID, _validNewUsername)).Returns(_testUser);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        AccountUser _actualUser = cryptoBL.UpdateUsername(_validCustomerID, _validNewUsername);

        Assert.Same(_testUser, _actualUser);
    }
    [Fact]
    public void UpdateNameValueTest(){
        int _validCustomerID = 1;
        string _validNewName = "New Name";
        AccountUser _testUser = new AccountUser(){
            ID = _validCustomerID,
            name = _validNewName,
        };
        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.UpdateName(_validCustomerID, _validNewName)).Returns(_testUser);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        AccountUser _actualUser = cryptoBL.UpdateName(_validCustomerID, _validNewName);

        Assert.Same(_testUser, _actualUser);
    }
    [Fact]
    public void UpdateAgeValueTest(){
        int _validCustomerID = 1;
        int _validNewAge = 19;
        AccountUser _testUser = new AccountUser(){
            ID = _validCustomerID,
            age = _validNewAge,
        };
        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.UpdateAge(_validCustomerID, _validNewAge)).Returns(_testUser);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);
        AccountUser _actualUser = cryptoBL.UpdateAge(_validCustomerID,_validNewAge);

        Assert.Same(_testUser, _actualUser);
    }
}
