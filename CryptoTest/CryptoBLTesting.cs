using System;
using Model;
using Moq;
using CryptoDL;
using CryptoBL;
using Xunit;

namespace CryptoBLTest;

public class CryptoBLTest{
    [Fact]
    public void AddToWalletValueTest(){
        decimal _validValue = 1.00m;
        int _validID = 1;
        Wallet _tWallet = new Wallet(){
            customerId = _validID,
            cash = _validValue,
        };

        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.AddtoWallet(1.00m, 1)).Returns(_tWallet);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);

        Wallet actualWallet = cryptoBL.AddtoWallet(1.00m, 1);

        _tWallet.customerId = _validID;
        _tWallet.cash = _validValue;

        Assert.Equal(_validValue, actualWallet.cash);
        Assert.Equal(_validID, actualWallet.customerId);
    }
    [Fact]
    public void PlaceOrderValueTest(){

    }
    
}
