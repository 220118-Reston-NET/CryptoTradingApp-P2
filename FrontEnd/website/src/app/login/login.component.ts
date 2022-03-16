import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: FormGroup = new FormGroup({
    username: new FormControl(''),
    password: new FormControl('')
  });
  submitted = false;
  isLoginFailed = false;
  errorMessage = "";

  constructor(private router: Router, private titleService: Title, private formBuilder: FormBuilder, private service:AccountService) { }

  ngOnInit(): void {
    this.titleService.setTitle("Login | CryptTrade");
    this.login = this.formBuilder.group(
      {
        username: [
          '',
          [
            Validators.required
          ]
        ],
        password: [
          '',
          [
            Validators.required
          ]
        ]
      }
    );
  }

  onSubmit(): void {
    this.submitted = true;
    const username = this.login.get("username")?.value;
    const password = this.login.get("password")?.value;

    this.service.loginUser(username, password).subscribe(result => {
      if(result.id == 0) {
        this.isLoginFailed = true;
        this.errorMessage = "Incorrect login details";
      } else {
        sessionStorage.setItem("username", result.username);
        this.router.navigate(["/account"]);
        this.service.isLoggedIn = true;
      }
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.login.controls;
  }

  reloadPage(): void {
    window.location.reload();
  }
}