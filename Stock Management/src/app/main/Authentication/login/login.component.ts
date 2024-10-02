import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from './login';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private loginService: LoginService,
              private router: Router) { }

  ngOnInit(): void {
    localStorage.removeItem("loginResponse");
  }

  public isLoading: boolean = false;

  public loginForm = new FormGroup({
      strUserName: new FormControl('', [Validators.required]),
      strPassword: new FormControl('', [Validators.required]),
    })

    public onSubmit(data:any)
    {
      this.isLoading = !this.isLoading;
      var self = this;
      this.loginService.ValidateUser(data).subscribe(
      res =>  
      {
        
        if(res.isSuccess)
        {
          this.isLoading = !this.isLoading;
          localStorage.setItem("loginResponse", res.Data);
          window.location.href = 'main/dashboard';
        }
      },
      err =>
      {
        this.isLoading = !this.isLoading;
      }
      )
    }
}
