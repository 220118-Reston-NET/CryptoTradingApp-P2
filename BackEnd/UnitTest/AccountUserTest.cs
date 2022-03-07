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
}