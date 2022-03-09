import { Component, OnInit } from '@angular/core';
import { DarkModeService } from 'angular-dark-mode';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  darkMode$: Observable<boolean> = this.darkModeService.darkMode$;

  constructor(private darkModeService: DarkModeService, private router:Router) { }

  onToggle(): void {
    this.darkModeService.toggle();
  }

  ngOnInit(): void {

  }

  goToAccount()
  {
    this.router.navigate(["/account"]);
  }

}
