import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AccountUser } from '../models/accountuser.model';
import { Wallet } from '../models/wallet.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  isLoggedIn = false;

  constructor(private http:HttpClient) { }

  addUser(user:AccountUser)
  {
    return this.http.post<AccountUser>('http://cryptotradingapp.azurewebsites.net/api/User/AddUser', user);
  }

  getAllUsers()
  {
    return this.http.get<AccountUser[]>("http://cryptotradingapp.azurewebsites.net/api/Admin/GetAllUsers");
  }

  loginUser(username:String, password:String)
  {
    return this.http.get<AccountUser>("http://cryptotradingapp.azurewebsites.net/api/User/Login?p_userName="+username+"&p_password="+password);
  }

  getWallet(user:AccountUser)
  {
    return this.http.get<Wallet>("http://cryptotradingapp.azurewebsites.net/api/User/ViewWallet?p_userID="+user.id);
  }

}