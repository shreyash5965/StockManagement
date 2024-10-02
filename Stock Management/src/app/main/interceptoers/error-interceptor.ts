import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    
  constructor(private router:Router){}
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    
    return next.handle(request).pipe(
      tap({
        next: (event) => {
            console.log(event);
          if (event instanceof HttpResponse) {
            if(event.status == 401) {
              alert('Unauthorize access!!!');
              localStorage.removeItem("loginResponse");
              window.location.href = '/';
            }
          }
          return event;
        },
        error: (error) => {
          console.log(error);
          
          if(error.status == 401) 
          {
            alert('Unauthorize access!!!')
            localStorage.removeItem("loginResponse");
            // window.location.href = "/";
          }
          else if(error.status == 404)
            alert('Page Not Found!!!')
          else if(error.status == 500)
            alert('Internal Server Error!!!')
          else
            alert('Something went wrong with API!!!')
        }
      }
      )
    );
  }
}
