import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { pipe } from 'rxjs';
import { SharedService } from '../../common/shared-service';
import { Home } from './home';
import { HomeService } from './home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private homeService: HomeService, private sharedService: SharedService, private router: Router) { }

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
      var newData = new Home();
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

  public routeData(){
    this.sharedService.changeMessage("TEST DATA");
    this.router.navigate(['main/aboutus']);
  }
}