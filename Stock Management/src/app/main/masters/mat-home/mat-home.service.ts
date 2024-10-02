import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Result } from 'src/app/common/Result';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MatHomeService {

  constructor(private httpClient: HttpClient) { }

  public GetData()
  {
    return this.httpClient.get<Result>(`${environment.apiURL}/User/GetData`)
  }

  public Add_Update_Data(data:any)
  {
    return this.httpClient.post<Result>(`${environment.apiURL}/User/Add_Update_Data`, data)
  }

  public DeleteUser(data:any)
  {
    return this.httpClient.post<Result>(`${environment.apiURL}/User/DeleteData`, data)
  }
}
