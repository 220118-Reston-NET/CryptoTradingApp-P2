using Model;
using Xunit;



namespace UnitTest;

public class AssetsTest
{
    [Fact]
    public void customerIdShouldSetValidData()
    {
        //Arrange
        Assets set = new Assets();
        int validId = 2;
    
        //Act
        set.customerId = validId;
    
        //Assert
        Assert.NotNull(set.customerId);
        Assert.Equal(validId, set.customerId);
    }

    [Fact]
    public void cryptoNameShouldSetValidData()
    {
        //Arrange
        Assets set = new Assets();
        string validCrypto = "Bitcoin";
    
        //Act
        set.cryptoName = validCrypto;
    
        //Assert
        Assert.NotNull(set.cryptoName);
        Assert.Equal(validCrypto, set.cryptoName);

    }

    [Fact]
    public void buyPriceShouldSetValidData()
    {
        //Arrange
        Assets set = new Assets();
        decimal validPrice = 50000;
    
        //Act
        set.buyPrice = validPrice;
    
        //Assert
        Assert.NotNull(set.buyPrice);
        Assert.Equal(validPrice, set.buyPrice);

    }

    [Fact]
    public void stopLossShouldSetValidData()
    {
        //Arrange
        Assets set = new Assets();
        decimal validStop = 40000;
    
        //Act
        set.stoploss = validStop;
    
        //Assert
        Assert.NotNull(set.stoploss);
        Assert.Equal(validStop, set.stoploss);

    }

    [Fact]
    public void takeProfitShouldSetValidData()
    {
        //Arrange
        Assets set = new Assets();
        decimal validProfit = 60000;
    
        //Act 
        set.takeprofit = validProfit;
    
        //Assert
        Assert.NotNull(set.takeprofit);
        Assert.Equal(validProfit, set.takeprofit);

    }

    //Need Unit Test for DateTime buyDate

    // [Fact]
    // public void TestName()
    // {
    //     // Given
    
    //     // When
    
    //     // Then
    // }
}