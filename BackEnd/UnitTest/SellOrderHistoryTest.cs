using System;
using Model;
using Xunit;



namespace UnitTest;

public class SellOrderHistoryTest
{
    [Fact]
    public void customerIdShouldSetValidData()
    {
        //Arrange
        SellOrderHistory order = new SellOrderHistory();
        int validID = 2;
    
        //Act
        order.customerId = validID;
    
        //Assert 
        Assert.NotNull(validID);
        Assert.Equal(validID, order.customerId);

    }

    [Fact]
    public void cryptoNameShouldSetValidData()
    {
        //Arrange 
        SellOrderHistory order = new SellOrderHistory();
        string validName = "Bitcoin";
    
        //Act
        order.cryptoName = validName;
    
        //Assert
        Assert.NotNull(validName);
        Assert.Equal(validName, order.cryptoName);

    }

    [Fact]
    public void sellPriceShouldSetValidData()
    {
        //Arrange 
        SellOrderHistory order = new SellOrderHistory();
        decimal validPrice = 20000;
    
        //Act
        order.sellPrice = validPrice;
    
        //Assert
        Assert.NotNull(validPrice);
        Assert.Equal(validPrice, order.sellPrice);

    }

    [Fact]
    public void sellDateShouldSetValidData()
    {
        //Arrange
        SellOrderHistory order = new SellOrderHistory();
        DateTime date = new DateTime(2022, 10, 4);
    
        //Act
        order.sellDate = date;
    
        //Assert
        Assert.NotNull(date);
        Assert.Equal(date, order.sellDate);

    }

    [Fact]
    public void quantityShouldSetValidData()
    {
        //Arrange
        SellOrderHistory order = new SellOrderHistory();
        decimal validQuant = 60;
    
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
        SellOrderHistory order = new SellOrderHistory();
        decimal validTotal = 10000;
    
        //Act
        order.total = validTotal;
    
        //Assert
        Assert.NotNull(validTotal);
        Assert.Equal(validTotal, order.total);
        
    }
}