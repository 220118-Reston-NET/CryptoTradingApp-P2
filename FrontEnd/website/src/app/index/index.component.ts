import { Component, OnInit } from '@angular/core';
import { Subscription, timer } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Title } from '@angular/platform-browser';

interface Coin {
  id: string;
  image: string;
  name: string;
  symbol: string;
  current_price: number;
  price_change_24h: number;
  price_change_percentage_24h: number;
  market_cap: number;
}

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  api: string =
    'https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=100&page=1&sparkline=false';

  subscription: Subscription = new Subscription;
  coins: Coin[] = [];
  filteredCoins: Coin[] = [];
  searchText = '';

  constructor(private http: HttpClient, private titleService: Title) {

  }

  convertDecimal(num:number) {
    return Math.round(num * 100) / 100;
  }

  searchCoin() {
    this.filteredCoins = this.coins.filter(
      (coin) => coin.name.toLowerCase().includes(this.searchText.toLowerCase()) ||
                coin.symbol.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }

  ngOnInit(): void {
    this.titleService.setTitle("Cryptocurrencies | CryptoTrader");
    this.subscription = timer(0, 10000).pipe(
      switchMap(() => this.http
      .get<Coin[]>(this.api))
    ).subscribe((res) => {
      this.coins = res;
      this.filteredCoins = this.coins;
      },
      (err) => console.log(err));
  }
}
