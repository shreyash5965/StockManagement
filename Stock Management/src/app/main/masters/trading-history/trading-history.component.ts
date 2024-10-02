import { Component, OnInit, ViewChild } from '@angular/core';
import { pipe } from 'rxjs';
import { TradingHistory } from './trading-history';
import { TradingHistoryService } from './trading-history.service';
import {MatPaginator} from '@angular/material/paginator';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-trading-history',
  templateUrl: './trading-history.component.html',
  styleUrls: ['./trading-history.component.scss']
})
export class TradingHistoryComponent implements OnInit {

  constructor(
    private tradingHistoryService: TradingHistoryService,
    private dialog: MatDialog) { }

  public isNew:boolean = true;
  public isOpened:boolean = false;
  public stockDetails: any;
  public displayedColumns: string[] = ['strtrading', 'dtetradedon', 'strstockname', 'strshortname', 'intquantity', 
  'intstockprice', 'totalamount'];
  // @ViewChild(MatPaginator) paginator: MatPaginator;

  ngOnInit(): void {
    this.GetData();
  }

  public stockData:any[] = [];

  public GetData()
  {
    var self = this;
    this.tradingHistoryService.GetHistoryData().subscribe(
      res =>  
      {
        this.stockData = JSON.parse(res.data);
        console.log(this.stockData);
      },
      err =>
      {
        alert(err);
      }
    )
  }

  public onCancel(data:any){
    
    this.isOpened = data;
    this.GetData();
  }
}