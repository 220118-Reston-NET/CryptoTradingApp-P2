using System;
using Model;
using Xunit;



namespace UnitTest;

public class AccountUserTest
{
    [Fact]
    public void IdShouldSetValidData()
    {
        //Arrange 
        AccountUser user = new AccountUser();
        int validID = 1;
    
        //Act
        user.ID = validID;
    
        //Assert
        Assert.NotNull(user.ID);
        Assert.Equal(validID, user.ID);
    }

    [Fact]
    public void UserNameShouldSetValidData()
    {
        //Arrange
        AccountUser user = new AccountUser();
        string validName = "bdawkins00";

        //Act
        user.username = validName;

        //Assert
        Assert.NotNull(user.username); 
        Assert.Equal(validName, user.username);

    }

    [Fact]
    public void NameShouldSetValidDate()
    {
        //Arrange
        AccountUser user = new AccountUser();
        string validName = "Jean Michel";

        //Act
        user.name = validName;
    
        //Assert
        Assert.NotNull(user.name);
        Assert.Equal(validName, user.name);

    }

    [Fact]
    public void AgeShouldSetValidData()
    {
        //Arrange
        AccountUser user = new AccountUser();
        int validAge = 24;

        //Act
        user.age = validAge;

        //Assert
        Assert.NotNull(user.age);
        Assert.Equal(validAge, user.age);

    }

    [Fact]
    public void isBannedShouldSetValidData()
    {
        //Arrange
        AccountUser user = new AccountUser();
        int validBanned = 2;

        //Act
        user.isBanned = validBanned;

        //Assert
        Assert.NotNull(user.isBanned);
        Assert.Equal(validBanned, user.isBanned);

    }

    [Fact]
    public void isAdminShouldSetValidData()
    {
        //Arrange
        AccountUser user = new AccountUser();
        int validAdmin = 1;

        //Act
        user.isAdmin = validAdmin;
    
        //Assert
        Assert.NotNull(user.isAdmin);
        Assert.Equal(validAdmin, user.isAdmin);

    }

    [Fact]
    public void dateCreatedShouldSetValidData()
    {
        //Arrange
        AccountUser user = new AccountUser();
        DateTime date = new DateTime(2022, 8, 3);
    
        //Act
        user.dateCreated = date;
    
        //Assert
        Assert.NotNull(date);
        Assert.Equal(date, user.dateCreated);
        
    }

    [Fact]
    public async void byteShouldSetValidData()
    {
        //Arrange
        AccountUser user = new AccountUser();
        byte[] validPass = { 0, 3, 5, 7, 7, 8};
    
        //Act
        user._password = validPass;
    
        //Assert
        Assert.NotNull(validPass);
        Assert.Equal(validPass, user._password);

    }
}