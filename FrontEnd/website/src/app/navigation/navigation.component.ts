import { Component, OnInit } from '@angular/core';
import { DarkModeService } from 'angular-dark-mode';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

import { TokenStorageService } from '../services/token-storage.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  darkMode$: Observable<boolean> = this.darkModeService.darkMode$;
  private roles: string[] = [];
  isLoggedIn = false;
  showAdminBoard = false;
  showModeratorBoard = false;
  username?: string;

  constructor(private darkModeService: DarkModeService, private router:Router, private tokenStorageService:TokenStorageService) { }

  onToggle(): void {
    this.darkModeService.toggle();
  }

  ngOnInit(): void {
    this.isLoggedIn = !!this.tokenStorageService.getToken();
    if (this.isLoggedIn) {
      const user = this.tokenStorageService.getUser();
      this.roles = user.roles;
      this.showAdminBoard = this.roles.includes('ROLE_ADMIN');
      this.showModeratorBoard = this.roles.includes('ROLE_MODERATOR');
      this.username = user.username;
    }
  }

  goToAccount()
  {
    this.router.navigate(["/account"]);
  }

  logout(): void {
    this.tokenStorageService.signOut();
    window.location.reload();
  }

}
