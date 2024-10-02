import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Result } from 'src/app/common/Result';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(private httpClient: HttpClient) { }

  public GetStockList()
  {
    return this.httpClient.get<Result>(`${environment.apiURL}/Common/GetStockList`)
  }
}
