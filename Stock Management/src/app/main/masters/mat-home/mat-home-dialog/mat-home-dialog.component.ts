import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatHome } from '../mat-home';
import { MatHomeService } from '../mat-home.service';

@Component({
  selector: 'app-mat-home-dialog',
  templateUrl: './mat-home-dialog.component.html',
  styleUrls: ['./mat-home-dialog.component.scss']
})
export class MatHomeDialogComponent implements OnInit {

  constructor(private homeService: MatHomeService) { }

  @Input() isNew:boolean = false;
  @Input() set model(data:MatHome)
  {
    
    this.userForm.reset(data);
  }

  @Output() Opened = new EventEmitter<boolean>();

  ngOnInit(): void {
  }

  public isLoading:boolean = false;
  public showPassword:boolean = false;

  public userForm = new FormGroup({
    intuserid: new FormControl(0),
    strusername: new FormControl('', [Validators.required]),
    strpassword: new FormControl(''),
    strfirstname: new FormControl('', [Validators.required]),
    strmiddlename: new FormControl(''),
    strlastname: new FormControl('', [Validators.required]),
    stremailid: new FormControl('', [Validators.required]),
    strcontactno: new FormControl('', [Validators.required]),
    dtdob: new FormControl(''),
  })

  public onSubmit(data:any){
    ;
    this.isLoading = true;
    data.strcontactno = data.strcontactno.toString();
    this.homeService.Add_Update_Data(data).subscribe(
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
    
    this.userForm.reset();
    this.Opened.emit(false);
  }

  public togglePassword(data:any)
  {
    
    this.showPassword = data;
  }
}
