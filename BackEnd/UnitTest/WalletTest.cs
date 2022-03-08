using Model;
using Xunit;



namespace UnitTest;

public class WalletTest
{
    [Fact]
    public void cashShouldSetValidData()
    {
        //Arrange
        Wallet wall = new Wallet();
        decimal validCash = 10000;
    
        //Act
        wall.cash = validCash;
    
        //Assert
        Assert.NotNull(wall.cash);
        Assert.Equal(validCash, wall.cash);
        
    }
}