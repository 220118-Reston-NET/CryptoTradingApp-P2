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

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    AccountComponent,
    IndexComponent,
    CryptoComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {path: "", component:IndexComponent},
      {path: "account", component:AccountComponent},
      {path: "crypto/:cryptoname", component: CryptoComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
