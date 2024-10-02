import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Home } from '../home';
import { HomeService } from '../home.service';

@Component({
  selector: 'app-home-dialog',
  templateUrl: './home-dialog.component.html',
  styleUrls: ['./home-dialog.component.scss']
})
export class HomeDialogComponent implements OnInit {

  constructor(private homeService: HomeService) { }

  @Input() isNew:boolean = false;
  @Input() set model(data:Home)
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
    dtedob: new FormControl(''),
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
