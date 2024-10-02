import { Component, OnInit, ViewChild } from '@angular/core';
import { pipe } from 'rxjs';
import { MatStockMaster } from './mat-stock-master';
import { MatStockMasterService } from './mat-stock-master.service';
import {MatPaginator} from '@angular/material/paginator';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import { DeleteDialogComponent } from '../delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-mat-stock-master',
  templateUrl: './mat-stock-master.component.html',
  styleUrls: ['./mat-stock-master.component.scss']
})
export class MatStockMasterComponent implements OnInit {

  constructor(
    private stockMasterService: MatStockMasterService,
    private dialog: MatDialog) { }

  public isNew:boolean = true;
  public isOpened:boolean = false;
  public stockDetails: any;
  public displayedColumns: string[] = ['strstockname', 'strshortname', 'intlowestprice', 
  'inthighestprice', 'islisted', 'strdescription', 'actions'];
  // @ViewChild(MatPaginator) paginator: MatPaginator;

  ngOnInit(): void {
    this.GetData();
  }

  public stockData:any[] = [];

  public GetData()
  {
    var self = this;
    this.stockMasterService.GetData().subscribe(
      res =>  
      {
        
        if(res.isSuccess)
        {
          this.stockData = JSON.parse(res.Data);
          console.log(this.stockData);
        }
      },
      err =>
      {
        alert(err);
      }
    )
  }

  public Add_Edit_Data(data: any)
  {
    
    this.isOpened = true
    if(data == null)
    {
      var newData = new MatStockMaster();
      this.isNew = true;
      this.stockDetails = newData;
    }
    else
    {
      this.isNew = false;
      this.stockDetails = data;
    }
  }

  public DeleteUser(data:any){
    // if(confirm("Are you sure, you want to delete this stock?"))
    // {
    //   this.stockMasterService.DeleteUser(data).subscribe(
    //     res =>
    //     {
          
    //       if(res.isSuccess)
    //       {
    //         alert(res.Message);
    //         this.GetData();
    //       }
    //     },
    //     err=> 
    //     {
    //       alert(err);
    //     })
    // }

    this.dialog.open(DeleteDialogComponent, {
      width: '350px',
      height: '200px',
      data: { headingText: 'Stock Master'}
    }).afterClosed()
    .subscribe(response => {
      if(response){
        this.stockMasterService.DeleteUser(data).subscribe(
          res =>
          {
            
            if(res.isSuccess)
            {
              alert(res.Message);
              this.GetData();
            }
          },
          err=> 
          {
            alert(err);
          })
     }
    });
  }

  public onCancel(data:any){
    
    this.isOpened = data;
    this.GetData();
  }
}