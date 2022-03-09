using System;
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
}