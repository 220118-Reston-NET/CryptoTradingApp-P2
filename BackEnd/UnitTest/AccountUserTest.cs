using Model;
using Xunit;



namespace UnitTest;

public class AccountUserTest
{
    [Fact]
    public void userNameShouldSetValidData()
    {
        //Arrange
        AccountUser use = new AccountUser();
        string validName = "bdawkins00";

        //Act
        use.username = validName;

        //Assert
        Assert.NotNull(use.username); 
        Assert.Equal(validName, use.username);
    }
}