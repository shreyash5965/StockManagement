import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatStockMaster } from '../mat-stock-master';
import { MatStockMasterService } from '../mat-stock-master.service';

@Component({
  selector: 'app-mat-stock-master-dialog',
  templateUrl: './mat-stock-master-dialog.component.html',
  styleUrls: ['./mat-stock-master-dialog.component.scss']
})
export class MatStockMasterDialogComponent implements OnInit {

  constructor(private stockMasterService: MatStockMasterService) { }

  @Input() isNew:boolean = false;
  @Input() set model(data:MatStockMaster)
  {
    this.stockForm.reset(data);
  }

  @Output() Opened = new EventEmitter<boolean>();

  ngOnInit(): void {
  }

  public isLoading:boolean = false;
  public showPassword:boolean = false;

  public stockForm = new FormGroup({
    intstockid: new FormControl(0),
    strstockname: new FormControl('', [Validators.required]),
    strshortname: new FormControl(''),
    strdescription: new FormControl(''),
    intlowestprice: new FormControl('', [Validators.required]),
    inthighestprice: new FormControl('', [Validators.required]),
    islisted: new FormControl(),
    dtelistedon: new FormControl(''),
  })

  public onSubmit(data:any){
    ;
    this.isLoading = true;
    data.intlowestprice = data.intlowestprice.toString();
    data.inthighestprice = data.inthighestprice.toString();
    this.stockMasterService.Add_Update_Data(data).subscribe(
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
