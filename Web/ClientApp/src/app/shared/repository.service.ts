import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { FileRequest } from './file-request.model';

//const httpOptions = {
//  headers: new HttpHeaders({
//    'Content-Type': 'application/json'
//  })
//};

@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  constructor(private http: HttpClient) { }

  uploadThisFile = (route: string, uploadedFile: File, options:any) => {
    
    var formData = new FormData();
    formData.append('uploadedFile', uploadedFile);

    return this.http.post<any>(this.createCompleteRoute(route, environment.apiUrl), formData, options).pipe(
      catchError(this.errorHandled)
    )
  }

  errorHandled(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }
}
