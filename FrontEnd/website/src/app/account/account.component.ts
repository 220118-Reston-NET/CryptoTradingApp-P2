import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
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

  settings: FormGroup = new FormGroup({
    newUsername: new FormControl(''),
    oldPassword: new FormControl(''),
    newPassword: new FormControl(''),
    confirmPassword: new FormControl('')
  });
  isChanging = false;
  isDeleting = false;
  submitted = false;
  isChangesFailed = false;
  isChangesSuccess = false;
  errorMessage = "";
  successMessage = "";

  constructor(private router:ActivatedRoute, private route:Router, private service:AccountService, private formBuilder: FormBuilder, private titleService: Title) {
    
  }

  ngOnInit(): void {
    const username = sessionStorage.getItem("username");
    this.service.getAllUsers().subscribe(result => {
      this.listOfUsers = result;
      this.listOfUsers.forEach((user: any) => {
        if(user.username == username) {
          this.currentUser = user;
          this.titleService.setTitle(username+" | CryptoTrader");
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
    this.settings = this.formBuilder.group(
      {
        newUsername: [
          '',
          [
            Validators.minLength(6),
            Validators.maxLength(20)
          ]
        ],
        oldPassword: [
          '',
          [
            Validators.required
          ]
        ],
        newPassword: [
          '',
          [
            Validators.minLength(6),
            Validators.maxLength(40)
          ]
        ],
        confirmPassword: [
          '',
          [
          ]
        ]
      },
      {
        validators: [this.match('newPassword', 'confirmPassword')]
      }
    );
  }

  doChanges(): void {
    this.isChanging = true;
  }

  doDelete(): void {
    this.isDeleting = true;
  }

  onSubmit(): void {
    this.submitted = true;
    if(this.settings.valid) {
      const username = this.settings.get("newUsername")?.value;
      const oldPassword = this.settings.get("oldPassword")?.value;
      const newPassword = this.settings.get("newPassword")?.value;
      const confirmPassword = this.settings.get("confirmPassword")?.value;
      
      if (this.isChanging) {
        if (username.length < 6) {
          this.service.loginUser(this.currentUser.username, oldPassword).subscribe(result => {
            if(result.id == 0) {
              this.isChangesFailed = true;
              this.errorMessage = "Incorrect login details...";
            } else {
              sessionStorage.setItem("username", result.username);
            }
          });
        } else {
          this.service.loginUser(this.currentUser.username, oldPassword).subscribe(result => {
            if(result.id == 0) {
              this.isChangesFailed = true;
              this.errorMessage = "Incorrect login details.";
            } else {
              this.service.updateUsername(this.currentUser, username).subscribe(res => {
                sessionStorage.setItem("username", res.username);
                this.successMessage = "Sucessfully changed username from "+ this.currentUser.username +" to "+ res.username +"!";
                this.currentUser = res;
                this.isChangesSuccess = true;
              },
              (err) => {
                this.isChangesFailed = true;
                this.errorMessage ="Someone already owns this username!";
              });
            }
          });
        }
      } else if(this.isDeleting) {
        this.service.loginUser(this.currentUser.username, oldPassword).subscribe(result => {
          if(result.id == 0) {
            this.isChangesFailed = true;
            this.errorMessage = "Incorrect login details.";
          } else {
            this.service.deleteAccount(this.currentUser).subscribe(res => {
              sessionStorage.removeItem("username");
              this.service.isLoggedIn = false;
              this.route.navigate(['/']);
            },
            (err) => {
              //this.isChangesFailed = true;
              //this.errorMessage ="Someone already owns this username!";
              sessionStorage.removeItem("username");
              this.service.isLoggedIn = false;
              this.route.navigate(['/']);
            });
          }
        });
      }
    }
  }

  get f(): { [key: string]: AbstractControl } {
    return this.settings.controls;
  }

  match(controlName: string, checkControlName: string): ValidatorFn {
    return (controls: AbstractControl) => {
      const control = controls.get(controlName);
      const checkControl = controls.get(checkControlName);
      if (checkControl?.errors && !checkControl.errors['matching']) {
        return null;
      }
      if (control?.value !== checkControl?.value) {
        controls.get(checkControlName)?.setErrors({ matching: true });
        return { matching: true };
      } else {
        return null;
      }
    };
  }

}
