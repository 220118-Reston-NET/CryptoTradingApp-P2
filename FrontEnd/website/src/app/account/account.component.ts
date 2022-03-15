import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  currentUser: any;
  listOfUsers: any = [];

  constructor(private router:ActivatedRoute, private service:AccountService) {
    
  }

  ngOnInit(): void {
    const username = sessionStorage.getItem("username");
    this.service.getAllUsers().subscribe(result => {
      this.listOfUsers = result;
      this.listOfUsers.forEach((user: any) => {
        if(user.username == username) {
          this.currentUser = user;
        }
      });
    });
  }

}
