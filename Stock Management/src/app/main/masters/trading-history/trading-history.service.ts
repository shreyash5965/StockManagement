import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Result } from 'src/app/common/Result';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TradingHistoryService {

  constructor(private httpClient: HttpClient) { }

  public GetHistoryData()
  {
    return this.httpClient.post<Result>(`${environment.apiURL}/UserTrading/GetHistoryData`, {});
  }
}
