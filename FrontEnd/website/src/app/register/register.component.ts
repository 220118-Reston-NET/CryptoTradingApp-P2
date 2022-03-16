import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountUser } from '../models/accountuser.model';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  signup: FormGroup = new FormGroup({
    fullname: new FormControl(''),
    username: new FormControl(''),
    age: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl('')
  });
  submitted = false;
  
  form: any = {
    name: null,
    username: null,
    age: null,
    password: null
  };
  isSignUpFailed = false;
  errorMessage = '';
  constructor(private router: Router, private formBuilder: FormBuilder, private service:AccountService) { }

  ngOnInit(): void {
    this.signup = this.formBuilder.group(
      {
        fullname: [
          '',
          [
            Validators.required,
            Validators.minLength(2),
            Validators.maxLength(50)
          ]
        ],
        username: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(20)
          ]
        ],
        age: ['',
          [
          Validators.required,
          Validators.pattern("^[0-9]*$")
          ]
        ],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.maxLength(40),
            Validators.pattern('(?=\\D*\\d)(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z]).{8,30}')
          ]
        ],
        confirmPassword: ['', Validators.required]
      },
      {
        validators: [this.match('password', 'confirmPassword')]
      }
    );
  }

  onSubmit(): void {
    this.submitted = true;
    if(this.signup.valid) {
      let user:AccountUser = {
        id: 0,
        username: this.signup.get("username")?.value,
        name: this.signup.get("fullname")?.value,
        age: this.signup.get("age")?.value,
        dateCreated: new Date(),
        isBanned: 0,
        isAdmin: 0,
        _password: this.signup.get("password")?.value
      }

      this.service.addUser(user).subscribe(result => {
        if(result) {
          sessionStorage.setItem("username", result.username);
          this.router.navigate(["/account"]);
          this.service.isLoggedIn = true;
        }
      },
      (err) => {
        this.isSignUpFailed = true;
        this.errorMessage = err.message;
      });
    } else {
      this.isSignUpFailed = true;
    }
  }

  onReset(): void {
    this.submitted = false;
    this.signup.reset();
  }
  
  get f(): { [key: string]: AbstractControl } {
    return this.signup.controls;
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