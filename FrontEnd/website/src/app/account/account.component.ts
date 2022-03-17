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
  userToEdit: any;
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
  admin: FormGroup = new FormGroup({
    username: new FormControl('')
  });

  isBanningSuccess = false;
  isBanningFailure = false;
  adminErrorMessage = "";
  isDeletingFailure = false;
  isDeletingSuccess = false;
  adminSuccessMessage = "";
  isDeletingAccount = false;
  isChanging = false;
  isDeleting = false;
  isBanning = false;
  submitted = false;
  isChangesFailed = false;
  isChangesSuccess = false;
  isSearching = false;
  adminSubmitted = false;
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
    this.admin = this.formBuilder.group(
      {
        username: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(20)
          ]
        ]
      });
  }

  doChanges(): void {
    this.isDeleting = false;
    this.isChanging = true;
    this.submitted = false;
    this.isChangesFailed = false;
    this.isChangesSuccess = false;
  }

  doDelete(): void {
    this.submitted = false;
    this.isChangesFailed = false;
    this.isChangesSuccess = false;
    this.isChanging = false;
    this.isDeleting = true;
  }

  clearInput(): void {
    this.settings.get("newUsername")?.value;
    this.settings.get("oldPassword")?.setValue("");
    this.settings.get("newPassword")?.setValue("");
    this.settings.get("confirmPassword")?.setValue("");
  }

  doDeleteAdmin(): void {
    this.isDeletingFailure = false;
    this.isDeleting = false;
    this.submitted = false;
    this.isChangesFailed = false;
    this.isChangesSuccess = false;
    this.isBanning = false;
    this.isSearching = false;
    this.isDeletingAccount = true;
    this.isDeletingSuccess = false;
    this.isBanningSuccess = false;
    this.isBanningFailure = false;
  }

  doBan(): void {
    this.isDeletingFailure = false;
    this.isDeleting = false;
    this.submitted = false;
    this.isChangesFailed = false;
    this.isChangesSuccess = false;
    this.isDeletingAccount = false;
    this.isDeletingSuccess = false;
    this.isBanning = true;
    this.isBanningSuccess = false;
    this.isBanningFailure = false;
  }

  doSearch(): void {
    this.isDeletingFailure = false;
    this.isDeleting = false;
    this.submitted = false;
    this.isChangesFailed = false;
    this.isChangesSuccess = false;
    this.isDeletingAccount = false;
    this.isBanning = false;
    this.isSearching = true;
    this.isDeletingSuccess = false;
    this.isBanningSuccess = false;
    this.isBanningFailure = false;
  }

  onAdminSubmit(): void {
    this.adminSubmitted = true;
    if(this.admin.valid) {
      const username = this.admin.get("username")?.value;
      this.listOfUsers.forEach((user: any) => {
        if(user.username == username) {
          this.userToEdit = user;
        }
      });

      if (this.isSearching) {

      } else if (this.isBanning) {
        this.service.banUser(this.userToEdit.id, this.currentUser).subscribe(result => {
          this.isBanningSuccess = true;
          this.adminSuccessMessage = "Successfully banned account "+ this.userToEdit.username;
        },
        (err) => {
          this.isBanningFailure = true;
          this.adminErrorMessage = "Could not ban account.";
        });
      } else if (this.isDeletingAccount) {
        this.service.deleteAccount(this.userToEdit).subscribe(result => {
          this.adminSuccessMessage = "Successfully deleted account "+ this.userToEdit.username;
          this.isDeletingSuccess = true;
        },
        (err) => {
          this.adminSuccessMessage = "Successfully deleted account "+ this.userToEdit.username;
          this.isDeletingSuccess = true;
        });
      }
    }
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
              this.clearInput();
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
                this.clearInput();
              },
              (err) => {
                this.isChangesFailed = true;
                this.errorMessage ="Someone already owns this username!";
              });
            }
          });
        }

        if (newPassword.length > 5 && confirmPassword.length > 5) {
          this.service.loginUser(this.currentUser.username, oldPassword).subscribe(result => {
            if(result.id == 0) {
              this.isChangesFailed = true;
              this.errorMessage = "Incorrect login details...";
            } else {
              this.service.updatePassword(this.currentUser, newPassword).subscribe(res => {
                this.isChangesSuccess = true;
                this.successMessage = "Successfully changed password!";
                this.clearInput();
              },
              (err) => {
                this.isChangesFailed = true;
                this.errorMessage ="Could not save password!";
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

  get a(): { [key: string]: AbstractControl } {
    return this.admin.controls;
  }

  isAdmin() {
    return this.currentUser.isAdmin == 1;
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