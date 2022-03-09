using System;
using Model;
using Xunit;



namespace UnitTest;

public class BuyOrderHistoryTest
{
    [Fact]
    public void customerIdShouldSetValidData()
    {
        //Arrange 
        BuyOrderHistory order = new BuyOrderHistory();
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
        BuyOrderHistory order = new BuyOrderHistory();
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
        BuyOrderHistory order = new BuyOrderHistory();
        decimal validPrice = 60000;
    
        //Act 
        order.buyPrice = validPrice;
    
        //Assert
        Assert.NotNull(order.buyPrice);
        Assert.Equal(validPrice, order.buyPrice);

    }

    [Fact]
    public void buyDateShouldSetValidData()
    {
        //Arrange
        BuyOrderHistory order = new BuyOrderHistory();
        DateTime date = new DateTime(2022, 8, 3);
    
        //Act
        order.buyDate = date;
    
        //Assert
        Assert.NotNull(date);
        Assert.Equal(date, order.buyDate);

    }

    [Fact]
    public void quantityShouldSetValidData()
    {
        //Arrange
        BuyOrderHistory order = new BuyOrderHistory();
        decimal validQuant = 1000;
    
        //Act
        order.quantity = validQuant;
    
        //Assert
        Assert.NotNull(validQuant);
        Assert.Equal(validQuant, order.quantity);

    }

    [Fact]
    public void totalShouldSetValidData()
    {
        //Arrange 
        BuyOrderHistory order = new BuyOrderHistory();
        decimal validTotal = 50;
    
        //Act
        order.total = validTotal;
    
        //Assert
        Assert.NotNull(validTotal);
        Assert.Equal(validTotal, order.total);
        
    }

}