using Model;
using Xunit;



namespace UnitTest;

public class CurrentPriceTest
{
    [Fact]
    public void currentPriceShouldSetValidData()
    {
        //Arrange
        CurrentPrice price = new CurrentPrice();
        decimal validPrice = 2000;
    
        //Act
        price.currentPrice = validPrice;
    
        //Assert
        Assert.NotNull(validPrice);
        Assert.Equal(validPrice, price.currentPrice);

    }

    [Fact]
    public void cryptoNameShouldSetValidData()
    {
        //Arrange
        CurrentPrice price = new CurrentPrice();
        string validName = "Polygon";
    
        //Act
        price.cryptoName = validName;
    
        //Assert
        Assert.NotNull(validName);
        Assert.Equal(validName, price.cryptoName);

    }

    [Fact]
    public void alphaShouldSetValidData()
    {
        //Arrange
        CurrentPrice price = new CurrentPrice();
        decimal validAlpha = 100;
    
        //Act
        price.alphaVal = validAlpha;
    
        //Assert
        Assert.NotNull(validAlpha);
        Assert.Equal(validAlpha, price.alphaVal);

    }

    [Fact]
    public void betaShouldSetValidData()
    {
        //Arrange 
        CurrentPrice price = new CurrentPrice();
        decimal validBeta = 200;
    
        //Act
        price.betaVal = validBeta;
    
        //Assert
        Assert.NotNull(validBeta);
        Assert.Equal(validBeta, price.betaVal);

    }

    [Fact]
    public void SandPShouldSetValidData()
    {
        //Arrange
        CurrentPrice price = new CurrentPrice();
        decimal validSandP = 500;    

        //Act 
        price.sandp500Val = validSandP;
    
        //Assert 
        Assert.NotNull(validSandP);
        Assert.Equal(validSandP, price.sandp500Val);

    }

    [Fact]
    public void randShouldSetValidData()
    {
        //Arrange 
        CurrentPrice price = new CurrentPrice();
        decimal validRand = 300;
    
        //Act
        price.randVal = validRand;
    
        //Assert
        Assert.NotNull(validRand);
        Assert.Equal(validRand, price.randVal);
        
    }
}