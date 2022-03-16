import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Assets } from '../models/assets.model';
import { BuyOrderHistory } from '../models/buyorderhistory.model';
import { SellOrderHistory } from '../models/sellorderhistory.model';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  currentUser: any;
  listOfUsers: any = [];
  buyOrders: BuyOrderHistory[] = [];
  sellOrders: SellOrderHistory[] = [];
  positions: Assets[] = [];

  constructor(private router:ActivatedRoute, private service:AccountService, private titleService: Title) {
    
  }

  ngOnInit(): void {
    const username = sessionStorage.getItem("username");
    this.service.getAllUsers().subscribe(result => {
      this.listOfUsers = result;
      this.listOfUsers.forEach((user: any) => {
        if(user.username == username) {
          this.currentUser = user;
          this.titleService.setTitle(username+" | CryptTrade");
          this.service.buyOrderHistory(user).subscribe(res => {
            this.buyOrders = res;
          });
          this.service.sellOrderHistory(user).subscribe(res => {
            this.sellOrders = res;
          });
          this.service.getAssets(user).subscribe(res => {
            this.positions = res;
          });
        }
      });
    });
  }

}
