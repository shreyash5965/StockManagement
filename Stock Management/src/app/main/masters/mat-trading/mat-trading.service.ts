import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Result } from 'src/app/common/Result';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MatTradingService {

  constructor(private httpClient: HttpClient) { }

  public Add_Update_Data(data:any)
  {
    return this.httpClient.post<Result>(`${environment.apiURL}/UserTrading/Add_Update_Data`, data)
  }
}
