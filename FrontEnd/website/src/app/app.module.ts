import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavigationComponent } from './navigation/navigation.component';
import { AccountComponent } from './account/account.component';
import { IndexComponent } from './index/index.component';
import { CryptoComponent } from './crypto/crypto.component';

import { NgChartsModule } from 'ng2-charts';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    AccountComponent,
    IndexComponent,
    CryptoComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgChartsModule,
    
    RouterModule.forRoot([
      {path: "", component:IndexComponent},
      {path: "account", component:AccountComponent},
      {path: "crypto/:cryptoname", component: CryptoComponent},
      {path: "register", component:RegisterComponent},
      {path: "login", component:LoginComponent},
      {path: "home", component:HomeComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
