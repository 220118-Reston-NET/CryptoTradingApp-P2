import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AccountUser } from '../models/accountuser.model';
import { Assets } from '../models/assets.model';
import { BuyOrderHistory } from '../models/buyorderhistory.model';
import { SellOrderHistory } from '../models/sellorderhistory.model';
import { Wallet } from '../models/wallet.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  isLoggedIn = false;

  constructor(private http:HttpClient) { }

  addUser(user:AccountUser)
  {
    return this.http.post<AccountUser>('https://cryptotradingapp.azurewebsites.net/api/User/AddUser', user);
  }

  getAllUsers()
  {
    return this.http.get<AccountUser[]>("https://cryptotradingapp.azurewebsites.net/api/Admin/GetAllUsers");
  }

  loginUser(username:String, password:String)
  {
    return this.http.get<AccountUser>("https://cryptotradingapp.azurewebsites.net/api/User/Login?p_userName="+username+"&p_password="+password);
  }

  getWallet(user:AccountUser)
  {
    return this.http.get<Wallet>("https://cryptotradingapp.azurewebsites.net/api/User/ViewWallet?p_userID="+user.id);
  }

  buyMarket(user:AccountUser, amount:number, cryptoName: string, cryptoPrice:number)
  {
    return this.http.post<AccountUser>("https://cryptotradingapp.azurewebsites.net/api/User/PlaceOrder?p_userID="+user.id+"&p_amount="+amount+"&_cryptoName="+cryptoName+"&_cryptoprice="+ cryptoPrice, user);
  }

  sellMarket(user:AccountUser, amount:number, cryptoName: string, cryptoPrice:number)
  {
    return this.http.post<AccountUser>("https://cryptotradingapp.azurewebsites.net/api/User/SellOrder?p_amount="+amount+"&p_CryptoName="+cryptoName+"&p_userID="+user.id+"&p_cryptoPrice="+ cryptoPrice, user);
  }

  stopLoss(user:AccountUser, cryptoName: string, amount:number)
  {
    return this.http.post<AccountUser>("https://cryptotradingapp.azurewebsites.net/api/User/UpdateStopLoss?p_userID="+user.id+"&p_amount="+amount+"&p_cryptoName="+ cryptoName, user);
  }

  takeProfit(user:AccountUser, cryptoName: string, amount:number)
  {
    return this.http.post<AccountUser>("https://cryptotradingapp.azurewebsites.net/api/User/UpdateProfit?p_userID="+user.id+"&p_amount="+amount+"&p_cryptoName="+ cryptoName, user);
  }

  buyOrderHistory(user:AccountUser)
  {
    return this.http.get<BuyOrderHistory[]>("https://cryptotradingapp.azurewebsites.net/api/Admin/BuyOrderHistory?p_userID="+user.id);
  }

  sellOrderHistory(user:AccountUser)
  {
    return this.http.get<SellOrderHistory[]>("https://cryptotradingapp.azurewebsites.net/api/Admin/SellOrderHistory?p_userID="+user.id);
  }

  getAssets(user:AccountUser)
  {
    return this.http.get<Assets[]>("https://cryptotradingapp.azurewebsites.net/api/Admin/ViewAssets?p_userID="+user.id);
  }

  updateUsername(user:AccountUser, newUsername: string)
  {
    return this.http.put<AccountUser>("https://cryptotradingapp.azurewebsites.net/api/User/UpdateUsername?p_userID="+user.id+"&p_userName="+newUsername, user);
  }

  deleteAccount(user: AccountUser)
  {
    return this.http.delete("https://cryptotradingapp.azurewebsites.net/api/User/DeleteUser?p_userID="+ user.id);
  }

  updatePassword(user: AccountUser, newPassword: string)
  {
    return this.http.put<AccountUser>("https://cryptotradingapp.azurewebsites.net/api/User/UpdatePassword?p_userName="+ user.username +"&p_password="+ newPassword, user);
  }

}