import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GraphCoin } from '../models/graph.model';
import { MarketCoin } from '../models/marketcoin.model';
import { CryptoService } from '../services/crypto.service';

import { ChartConfiguration, ChartEvent, ChartType } from 'chart.js';
import { AccountService } from '../services/account.service';
import { Subscription, switchMap, timer } from 'rxjs';

@Component({
  selector: 'app-crypto',
  templateUrl: './crypto.component.html',
  styleUrls: ['./crypto.component.css'],
})
export class CryptoComponent implements OnInit {

  subscription: Subscription = new Subscription;
  cryptoName:string | null = "No Crypto Selected";
  coin: MarketCoin;
  graph: GraphCoin;

  public lineChartData: ChartConfiguration['data'];

  public lineChartOptions: ChartConfiguration['options'] = {
    elements: {
      line: {
        tension: 0.5
      }
    },
    scales: {
      // We use this empty structure as a placeholder for dynamic theming.
      x: {},
      'y-axis-0':
        {
          position: 'left',
        }
    }
  };

  public lineChartType: ChartType = 'line';

  constructor(private router:ActivatedRoute, private service:CryptoService, public account:AccountService) {
    this.coin = {
      id: '',
      image: '',
      name: '',
      current_price: 0,
      price_change_24h: 0,
      price_change_percentage_24h: 0
    };
    this.graph = {
      prices: []
    };
    this.lineChartData = {
      datasets: [
        {
          data: [ 65, 59, 80, 81, 56, 55, 40 ],
          label: 'Series A',
          backgroundColor: 'rgba(148,159,177,0.2)',
          borderColor: 'rgba(148,159,177,1)',
          pointBackgroundColor: 'rgba(148,159,177,1)',
          pointBorderColor: '#fff',
          pointHoverBackgroundColor: '#fff',
          pointHoverBorderColor: 'rgba(148,159,177,0.8)',
          fill: 'origin',
        }
      ],
      labels: [ 'January', 'February', 'March', 'April', 'May', 'June', 'July' ]
    };
  }

  convertDecimal(num:number) {
    return Math.round(num * 100) / 100;
  }

  ngOnInit(): void {
    this.cryptoName = this.router.snapshot.paramMap.get("cryptoname");
    this.subscription = timer(0, 10000).pipe(
      switchMap(() =>
    this.service.getCoinByName(this.cryptoName))).subscribe((res) => {
      this.coin = res[0];
    },
    (err) => console.log(err));

    this.subscription = timer(0, 60000).pipe(
        switchMap(() =>
    this.service.loadDailyChart(this.cryptoName))).subscribe((res) => {
      this.graph = res;

      var time = this.graph.prices.map(x => x[0]);
      var date: string[] = [];
      time.forEach(function(t) {
          var read = new Date(t).toLocaleString();
          date.push(read);
      });

      this.lineChartData = {
        datasets: [
          {
            data: this.graph.prices,
            label: this.coin.name,
            backgroundColor: 'rgba(148,159,177,0.2)',
            borderColor: 'rgba(148,159,177,1)',
            pointBackgroundColor: 'rgba(148,159,177,1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(148,159,177,0.8)',
            fill: 'origin',
          }
        ],
        labels: date
      };
    },
    (err) => console.log(err));
  }
}