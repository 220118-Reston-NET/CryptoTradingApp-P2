using Model;
using Xunit;



namespace UnitTest;

public class NotificationTest
{
    [Fact]
    public void cryptoNameShouldSetValidData()
    {
        //Arrange
        Notification noti = new Notification();
        string validName = "Ethereum";
    
        //Act
        noti.cryptoName = validName;
    
        //Assert
        Assert.NotNull(noti.cryptoName);
        Assert.Equal(validName, noti.cryptoName);

    }

    [Fact]
    public void alertPriceShouldSetValidData()
    {
        //Arrange
        Notification noti = new Notification();
        decimal validAlert = 35000;
    
        //Act
        noti.alertPrice = validAlert;
    
        //Assert
        Assert.NotNull(noti.alertPrice);
        Assert.Equal(validAlert, noti.alertPrice);
        
    }
}