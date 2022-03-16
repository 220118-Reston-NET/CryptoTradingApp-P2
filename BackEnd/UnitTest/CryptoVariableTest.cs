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

    [Fact]
    public void betaShouldSetValidData()
    {
        //Arrange
        CryptoVariables crypto = new CryptoVariables();
        decimal validBeta = 200;
    
        //Act
        crypto.betaVal = validBeta;
    
        //Assert
        Assert.NotNull(crypto.betaVal);
        Assert.Equal(validBeta, crypto.betaVal);

    }

    [Fact]
    public void sandShouldSetValidData()
    {
        //Arrange
        CryptoVariables crypto = new CryptoVariables();
        decimal validSand = 300;
    
        //Act
        crypto.sandp500Val = validSand;
    
        //Assert 
        Assert.NotNull(crypto.sandp500Val);
        Assert.Equal(validSand, crypto.sandp500Val);

    }

    [Fact]
    public void randShouldSetValidData()
    {
        //Arrange
        CryptoVariables crypto = new CryptoVariables();
        decimal validRand = 500;
    
        //Act
        crypto.randVal = validRand;
    
        //Assert 
        Assert.NotNull(crypto.randVal);
        Assert.Equal(validRand, crypto.randVal);

    }

    [Fact]
    public void calculatedShoudSetValidData()
    {
        //Arrange 
        CryptoVariables crypto = new CryptoVariables();
        float validCalc = 55;
    
        //Act
        crypto.calculated = validCalc;
    
        //Assert
        Assert.NotNull(crypto.calculated);
        Assert.Equal(validCalc, crypto.calculated);
        
    }
}