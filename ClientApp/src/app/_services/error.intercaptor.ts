import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, catchError, pipe, throwError } from "rxjs";




export class ErrorInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req)
      .pipe(catchError((error: HttpErrorResponse) =>{
          console.log(error);
          if(error.status===400)
          {
            if(error.error){
              const serverError=error;
              let errorMessage='';
              for(const key in serverError.error){
                errorMessage +=serverError.error[key]+ '\n';

              }
              return throwError(errorMessage);
            }
            return throwError(error.error.message);
          }

          return throwError(error.statusText);



      }))
  }

}
