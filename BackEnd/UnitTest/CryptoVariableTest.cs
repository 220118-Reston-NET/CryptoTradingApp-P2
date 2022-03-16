using System;
using Model;
using Xunit;



namespace UnitTest;

public class CryptoVariablesTest
{
    [Fact]
    public void PriceShouldSetValidData()
    {
        //Arrange
        CryptoVariables crypto = new CryptoVariables();
        decimal validPrice = 10000;

        //Act
        crypto.currentPrice = validPrice;
    
        //Assert
        Assert.NotNull(crypto.currentPrice);
        Assert.Equal(validPrice, crypto.currentPrice);

    }

    [Fact]
    public void cryptoNameShouldSetvalidData()
    {
        //Arrange
        CryptoVariables crypto = new CryptoVariables();
        string validName = "bitcoin";
    
        //Act
        crypto.cryptoName = validName;
    
        //Assert
        Assert.NotNull(crypto.cryptoName);
        Assert.Equal(validName, crypto.cryptoName);

    }

    [Fact]
    public void alphaShouldSetValidData()
    {
        //Arrange
        CryptoVariables crypto = new CryptoVariables();
        decimal validAlpha = 100;
    
        //Act
        crypto.alphaVal = validAlpha;
    
        //Assert
        Assert.NotNull(crypto.alphaVal);
        Assert.Equal(validAlpha, crypto.alphaVal);
        
    }
}