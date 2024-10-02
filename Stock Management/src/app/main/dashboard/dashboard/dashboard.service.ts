import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Result } from 'src/app/common/Result';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private httpClient: HttpClient) { }
  
  public GetDashboardData()
  {
    return this.httpClient.get<Result>(`${environment.apiURL}/UserTrading/GetDashboardData`)
  }
}
