using System;
using System.Collections.Generic;
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

        Assert.Equal(_validValue, actualWallet.cash);
        Assert.Equal(_validID, actualWallet.customerId);
    }
    [Fact]
    public void PlaceOrderValueTest(){

    }
    [Fact]
    public void ViewWalletValueTest(){
        int _validID = 1;
        decimal _validValue = 1.00m;
        Wallet _tWallet = new Wallet(){
            customerId = _validID,
            cash = _validValue,
        };

        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.SelectWalletbyCustomer(1)).Returns(_tWallet);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);

        Wallet actualWallet = cryptoBL.ViewWallet(1);

        Assert.Equal(_validID, actualWallet.customerId);
        Assert.Equal(_validValue, actualWallet.cash);
    }
    [Fact]
    public void ViewAssetsValueTest(){
        int _validID = 1;
        string _validName = "Crypto";
        decimal _validBuyPrice = 1.00m;
        DateTime _validBuyDate = new DateTime(2022,3,8);
        decimal _validStopLoss = 2.00m;
        decimal _validTakeProfit = 3.00m;
        decimal _validCoinQuantity = 4.00m;
        Assets _tAssets = new Assets(){
            customerId = _validID,
            cryptoName = _validName,
            buyPrice = _validBuyPrice,
            buyDate = _validBuyDate,
            stoploss = _validStopLoss,
            takeprofit = _validTakeProfit,
            coinQuantity = _validCoinQuantity,
        };

        List<Assets> expectedListOfAssets = new List<Assets>();
        expectedListOfAssets.Add(_tAssets);

        Mock<IRepository> mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.GetAssetsbyCustomer(1)).Returns(expectedListOfAssets);
        ICryptoClassBL cryptoBL = new CryptoClassBL(mockRepo.Object);

        List<Assets> actualListOfAssets = cryptoBL.ViewAssets(1);

        Assert.Same(expectedListOfAssets, actualListOfAssets);
    }
}
