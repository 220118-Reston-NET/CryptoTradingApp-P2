import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GraphCoin } from '../models/graph.model';
import { MarketCoin } from '../models/marketcoin.model';

@Injectable({
  providedIn: 'root'
})
export class CryptoService{

  constructor(private http:HttpClient) { }

  getCoinByName(cryptoName:string|null) : Observable<MarketCoin[]>
  {
    let cryptoNameString:string = <string>cryptoName;
    cryptoNameString = cryptoNameString.toLowerCase();
    return this.http.get<MarketCoin[]>(`https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&ids=${cryptoNameString}&order=market_cap_desc&per_page=100&page=1&sparkline=false`);
  }

  loadChart(cryptoName:string|null) : Observable<GraphCoin>
  {
    let cryptoNameString:string = <string>cryptoName;
    cryptoNameString = cryptoNameString.toLowerCase();
    return this.http.get<GraphCoin>(`https://api.coingecko.com/api/v3/coins/${cryptoNameString}/market_chart?vs_currency=usd&days=14&interval=daily`);
  }
}