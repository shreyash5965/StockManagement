import { HttpErrorResponse, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { map, Observable } from "rxjs";

@Injectable()
export class HeaderInterceptor implements HttpInterceptor 
{
  intercept (req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> 
  {
    var token = localStorage.getItem("loginResponse");
    
    if(token != null)
    {
        token = (JSON.parse(token)[0].token).toString()
        console.log(token)
    }

    req = req.clone({
        headers: new HttpHeaders({
        'Content-Type':'application/json',
        'accept':'*/*',
        'Authorization': "Bearer " + (token == null ? "" : token)
        })
    });

    return next.handle(req);
  }
}