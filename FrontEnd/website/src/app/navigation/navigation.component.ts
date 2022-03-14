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

  constructor(private darkModeService: DarkModeService, private router:Router, public service:AccountService) { }

  onToggle(): void {
    this.darkModeService.toggle();
  }

  ngOnInit(): void {
    if (sessionStorage.length == 1)
      this.service.isLoggedIn = true;
  }

  logout(): void {
    sessionStorage.removeItem("username");
    this.router.navigate(["/login"]);
    this.service.isLoggedIn = false;
  }

}
