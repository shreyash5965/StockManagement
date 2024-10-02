import { Component, OnInit } from '@angular/core';
import { pipe } from 'rxjs';
import { MatHome } from './mat-home';
import { MatHomeService } from './mat-home.service';

@Component({
  selector: 'app-mat-home',
  templateUrl: './mat-home.component.html',
  styleUrls: ['./mat-home.component.scss']
})
export class MatHomeComponent implements OnInit {

  constructor(private homeService: MatHomeService) { }

  public isNew:boolean = true;
  public isOpened:boolean = false;
  public userDetails: any;


  ngOnInit(): void {
    this.GetData();
  }

  public userData:any[] = [];

  public GetData()
  {
    var self = this;
    this.homeService.GetData().subscribe(
      res =>  
      {
        
        if(res.isSuccess)
        {
          this.userData = JSON.parse(res.Data);
          // localStorage.setItem("loginResponse", res.Data);
          // self.router.navigate(['main/home']);
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
      var newData = new MatHome();
      this.isNew = true;
      this.userDetails = newData;
    }
    else
    {
      this.isNew = false;
      this.userDetails = data;
    }
  }

  public DeleteUser(data:any){
    if(confirm("Are you sure, you want to delete this user?"))
    {
      this.homeService.DeleteUser(data).subscribe(
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
  }

  public onCancel(data:any){
    
    this.isOpened = data;
    this.GetData();
  }
}