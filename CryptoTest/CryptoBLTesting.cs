using System;
using Model;
using Xunit;

namespace CryptoBLTest;

public class CryptoBLTest{
    [Fact]
    public void NewUserValueTest(){
        AccountUser _tNewUser = new AccountUser();
        int _validID = 1;
        string _validUserName = "UserName";
        string _validName = "Name";
        int _validAge = 18;
        DateTime _validDateTime = new DateTime(2022,3,8);
        int _validIsBanned = 0;
        int _validIsAdmin = 0;

        _tNewUser.ID = _validID;
        _tNewUser.username = _validUserName;
        _tNewUser.name = _validName;
        _tNewUser.age = _validAge;
        _tNewUser.dateCreated = _validDateTime;
        _tNewUser.isBanned = _validIsBanned;
        _tNewUser.isAdmin = _validIsAdmin;

        Assert.NotNull(_tNewUser.ID);
        Assert.NotNull(_tNewUser.username);
        Assert.NotNull(_tNewUser.name);
        Assert.NotNull(_tNewUser.age);
        Assert.NotNull(_tNewUser.dateCreated);
        Assert.NotNull(_tNewUser.isBanned);
        Assert.NotNull(_tNewUser.isAdmin);
        Assert.Equal(_validID, _tNewUser.ID);
        Assert.Equal(_validUserName, _tNewUser.username);
        Assert.Equal(_validName, _tNewUser.name);
        Assert.Equal(_validAge, _tNewUser.age);
        Assert.Equal(_validDateTime, _tNewUser.dateCreated);
        Assert.Equal(_validIsBanned, _tNewUser.isBanned);
        Assert.Equal(_validIsAdmin, _tNewUser.isAdmin);
    }
}
