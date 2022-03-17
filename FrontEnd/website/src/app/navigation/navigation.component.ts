import { Component, OnInit } from '@angular/core';
import { DarkModeService } from 'angular-dark-mode';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  darkMode$: Observable<boolean> = this.darkModeService.darkMode$;
  currentUser: any;
  currentCash: any;
  listOfUsers: any = [];

  constructor(private darkModeService: DarkModeService, private router:Router, public service:AccountService) { }

  onToggle(): void {
    this.darkModeService.toggle();
  }

  update(): void {
    if (sessionStorage.length == 1) {
      this.service.isLoggedIn = true;
      const username = sessionStorage.getItem("username");
      this.service.getAllUsers().subscribe(result => {
        this.listOfUsers = result;
        this.listOfUsers.forEach((user: any) => {
          if(user.username == username) {
            this.currentUser = user;
            this.service.getWallet(this.currentUser).subscribe(res => {
              this.currentCash = res.cash;
            });
          }
        });
      });
    }
  }

  ngOnInit(): void {
    if (sessionStorage.length == 1) {
      this.service.isLoggedIn = true;
      const username = sessionStorage.getItem("username");
      this.service.getAllUsers().subscribe(result => {
        this.listOfUsers = result;
        this.listOfUsers.forEach((user: any) => {
          if(user.username == username) {
            this.currentUser = user;
            this.service.getWallet(this.currentUser).subscribe(res => {
              this.currentCash = res.cash;
            });
          }
        });
      });
    }
  }

  logout(): void {
    sessionStorage.removeItem("username");
    this.router.navigate(["/login"]);
    this.service.isLoggedIn = false;
  }

}
