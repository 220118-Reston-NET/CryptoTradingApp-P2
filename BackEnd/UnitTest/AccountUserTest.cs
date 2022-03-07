using Model;
using Xunit;



namespace UnitTest;

public class AccountUserTest
{
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

    //Need Unit Test for DateTime dateCreated

    // [Fact]
    // public void TestName()
    // {
    //     // Given
    
    //     // When
    
    //     // Then
    // }

    //Need Unit Test for byte _password

    // [Fact]
    // public void TestName()
    // {
    //     // Given
    
    //     // When
    
    //     // Then
    // }
}