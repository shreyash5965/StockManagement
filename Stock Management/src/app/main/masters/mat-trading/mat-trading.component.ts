import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CommonService } from '../../common/common.service';
import { MatStockMaster } from '../mat-stock-master/mat-stock-master';
import { MatTrading } from './mat-trading';
import { MatTradingService } from './mat-trading.service';

@Component({
  selector: 'app-mat-trading',
  templateUrl: './mat-trading.component.html',
  styleUrls: ['./mat-trading.component.scss']
})
export class MatTradingComponent implements OnInit {

  constructor(private matTradingService: MatTradingService,
    private commonService: CommonService) { }

  @Input() isNew:boolean = true;
  @Output() Opened = new EventEmitter<boolean>();

  isLoading:boolean = false;
  min: string = "";
  max: string = "";
  stockList: MatStockMaster[] = [];
  ngOnInit(): void {
    this.getStockList();
  }

  public stockForm = new FormGroup({
    intstockid: new FormControl([Validators.required]),
    intquantity: new FormControl('', [Validators.required]),
    strstockname: new FormControl(),
    strshortname: new FormControl(),
    strtradetype: new FormControl('Delivery'),
    intstockprice: new FormControl('', [Validators.required]),
    strtrading: new FormControl('Buy'),
  })

  getStockList(){
    this.commonService.GetStockList().subscribe(
      res =>
      {
        if(res.isSuccess)
        {
          this.stockList = JSON.parse(res.Data);
        }
      },
      err=> 
      {
        this.isLoading = false;
      })
  }

  onSelectStock(data: any){

    if(data != 0){
      var stockDetail = this.stockList.find(s => s.intstockid == data);
  
      if(stockDetail != null){
        this.stockForm.controls["strstockname"].setValue(stockDetail.strstockname);
        this.stockForm.controls["strshortname"].setValue(stockDetail.strshortname);
        this.min = stockDetail.intlowestprice;
        this.max = stockDetail.inthighestprice;
      }
    }
    else{
      this.min = "";
      this.max = "";
    }

  }

  public onSubmit(data:any){
    this.isLoading = true;
    data.intquantity = data.intquantity.toString();
    data.intstockprice = data.intstockprice.toString();

    this.matTradingService.Add_Update_Data(data).subscribe(
      res =>
      {
        if(res.isSuccess)
        {
          alert(res.Message);
          this.onCancel();
        }
      },
      err=> 
      {
        this.isLoading = false;
      })
  }

  public onCancel()
  {
    this.stockForm.reset();
    this.Opened.emit(false);
  }

  // public onChangeListedFlag(data:any)
  // {
  //   if(data)
  //   {
  //     this.stockForm.controls.dtelistedon.setValidators([Validators.required]);
  //   }
  //   else
  //   {
  //     this.stockForm.controls.dtelistedon.setValue('');
  //     this.stockForm.controls.dtelistedon.setValidators(null);
  //   }
  // }
}
