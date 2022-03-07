using Model;
using Xunit;



namespace UnitTest;

public class OrderHistoryTest
{
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

    //Need a Unit Test for DateTime buyDate

    // [Fact]
    // public void TestName()
    // {
    //     // Given
    
    //     // When
    
    //     // Then
    // }

    //Need a Unit Test for DateTime sellDate

    // [Fact]
    // public void TestName()
    // {
    //     // Given
    
    //     // When
    
    //     // Then
    // }
}