import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface MarketCoin {
  image?: string;
  name?: string;
  current_prirce?: number;
  price_change_24h?: number;
  price_change_percentage_24h?: number;
}

@Component({
  selector: 'app-crypto',
  templateUrl: './crypto.component.html',
  styleUrls: ['./crypto.component.css']
})
export class CryptoComponent implements OnInit {

  cryptoName:string | null = "No Crypto Selected";
  coin:MarketCoin;

  constructor(private router:ActivatedRoute, private http: HttpClient) {
    this.coin = {

    };
  }

  getCoinByName(cryptoName:string|null) : Observable<MarketCoin>
  {
    let cryptoNameString:string = <string>cryptoName;
    cryptoNameString = cryptoNameString.toLowerCase();
    return this.http.get<MarketCoin>('https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids=${cryptoNameString}&order=market_cap_desc&per_page=100&page=1&sparkline=false');
  }

  convertDecimal(num:number) {
    return Math.round(num * 100) / 100;
  }

  ngOnInit(): void {
    this.cryptoName = this.router.snapshot.paramMap.get("cryptoname");
    this.getCoinByName(this.cryptoName).subscribe(result => {
      this.coin = result;
    });
  }

}
