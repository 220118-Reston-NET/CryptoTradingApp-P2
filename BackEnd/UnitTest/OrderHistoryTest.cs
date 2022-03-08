using System;
using Model;
using Xunit;



namespace UnitTest;

public class OrderHistoryTest
{
    [Fact]
    public void customerIdShouldSetValidData()
    {
        //Arrange 
        OrderHistory order = new OrderHistory();
        int validID = 4;
    
        //Act
        order.customerId = validID;
    
        //Assert
        Assert.NotNull(order.customerId);
        Assert.Equal(validID, order.customerId);
    }

    [Fact]
    public void cryptoNameShouldSetValidData()
    {
        //Arrange
        OrderHistory order = new OrderHistory();
        string validName = "Cardano";
    
        //Act
        order.cryptoName = validName;
    
        //Assert
        Assert.NotNull(order.cryptoName);
        Assert.Equal(validName, order.cryptoName);

    }

    [Fact]
    public void buyPriceShouldSetValidData()
    {
        //Arrange
        OrderHistory order = new OrderHistory();
        decimal validPrice = 60000;
    
        //Act 
        order.buyPrice = validPrice;
    
        //Assert
        Assert.NotNull(order.buyPrice);
        Assert.Equal(validPrice, order.buyPrice);

    }

    [Fact]
    public void sellPriceShouldSetValidData()
    {
        //Arrange
        OrderHistory order = new OrderHistory();
        decimal validSell = 55000;
    
        //Act
        order.sellPrice = validSell;
    
        //Arrange
        Assert.NotNull(order.sellPrice);
        Assert.Equal(validSell, order.sellPrice);

    }

    [Fact]
    public void totalReturnShouldSetValidData()
    {
        //Arrange
        OrderHistory order = new OrderHistory();
        decimal validTotal = 45000;
    
        //Act
        order.totalReturn = validTotal;
    
        //Assert
        Assert.NotNull(order.totalReturn);
        Assert.Equal(validTotal, order.totalReturn);

    }

    [Fact]
    public void buyDateShouldSetValidData()
    {
        //Arrange
        OrderHistory order = new OrderHistory();
        DateTime date = new DateTime(2022, 8, 3);
    
        //Act
        order.buyDate = date;
    
        //Assert
        Assert.NotNull(date);
        Assert.Equal(date, order.buyDate);

    }

    [Fact]
    public void sellDateShouldSetValidData()
    {
        //Arrange
        OrderHistory order = new OrderHistory();
        DateTime date = new DateTime(2022, 9, 4);
    
        //Act 
        order.sellDate = date;
    
        //Assert
        Assert.NotNull(date);
        Assert.Equal(date, order.sellDate);

    }
}