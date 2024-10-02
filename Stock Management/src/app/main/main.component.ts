import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
    console.log("Dark" , this.isDarkModeOn)
    this.onDarkModeToggle();
    this.GetMenuList();
  }

  @ViewChild('sideNav')
  sideNav!: ElementRef;
  public menuList: any[] = [];
  public loginResponse:any = localStorage.getItem("loginResponse");
  public userName: string = this.loginResponse != null ? JSON.parse(this.loginResponse)[0].strusername : ""
  // public FullName: string = this.loginResponse != null ? (JSON.parse(this.loginResponse)[0].strfirstname + " " + JSON.parse(this.loginResponse)[0].strlastname) : ""
  public FullName: string = this.loginResponse != null ? (JSON.parse(this.loginResponse)[0].strfirstname) : ""

  public isDarkModeOn : boolean = localStorage.getItem("isDarkMode") != null ? (localStorage.getItem("isDarkMode") == 'true' ? true : false) : false;

  GetMenuList(){
    this.menuList.push({ pageName: 'Dashboard', pageLink: '/dashboard', iconName: 'dashboard'});
    this.menuList.push({ pageName: 'Stock Master', pageLink: '/mat-stock-master', iconName: 'home'});
    this.menuList.push({ pageName: 'Stock Master', pageLink: '/mat-stock-master', iconName: 'history'});

    console.log(this.menuList);
  }

  openSidenav(){
    debugger
    this.sideNav.nativeElement.style.marginLeft = "50%";
    this.sideNav.nativeElement.style.transition = "1s";
    // data? = "translate(50%)";
  }

  onDarkModeToggle(){
    localStorage.setItem("isDarkMode", this.isDarkModeOn.toString());
    this.isDarkModeOn ? document.body.classList.add('dark') : document.body.classList.remove('dark');
  }

  logout(){
    localStorage.removeItem("loginResponse");
    window.location.href = '/';
  }
}
