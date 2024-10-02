import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Result } from 'src/app/common/Result';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpClient: HttpClient) { }
  
  public ValidateUser(data:any)
  {
    return this.httpClient.post<Result>(`${environment.apiURL}/Login/ValidateUser`, data)
  }
}
