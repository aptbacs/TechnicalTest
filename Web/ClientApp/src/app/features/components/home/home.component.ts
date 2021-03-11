import { RepositoryService } from '../../../shared/repository.service';
import { environment } from '../../../../environments/environment';
import { Component, OnInit, ElementRef, ViewChild, Output, EventEmitter } from '@angular/core';
import { HttpEventType, HttpClient, HttpEvent } from '@angular/common/http';
import { FileResponse } from '../../../shared/file-response.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  @ViewChild('UploadFileInput') uploadFileInput!: ElementRef;
  @Output() public onUploadFinished = new EventEmitter();
  fileName = 'Select File';
  chosenFile: any;
  fileResponse: FileResponse = new FileResponse;
  errorMessage: any;
  progress: number = 0;
  message: string = "";

  constructor(private repoService : RepositoryService ) { }

  ngOnInit(): void {
    
  }

  public executeSelectedChange = (event:any) => {
    console.log(event);
  }

  public uploadFile() {
    var nativeElement: HTMLInputElement = this.uploadFileInput.nativeElement;
    if (nativeElement.files !== null) {
      this.fileName = nativeElement.files[0].name;
      console.log(`=========== New Chosen File is ${nativeElement.files[0].name} and size:${nativeElement.files[0].size} ==============`);
      this.repoService.uploadThisFile(environment.apiFileUploadRoute, nativeElement.files[0], { reportProgress: true, observe: 'events' })
        .subscribe((event: HttpEvent<any>) => {
                switch (event.type) {
                  case HttpEventType.Sent:
                    console.log('Request has been made!');
                    break;
                  case HttpEventType.ResponseHeader:
                    console.log('Response header has been received!');
                    break;
                  case HttpEventType.UploadProgress:
                    if(event.total)
                    this.progress = Math.round(event.loaded / event.total * 100);
                    console.log(`Uploaded! ${this.progress}%`);
                    break;
                  case HttpEventType.Response:
                    console.log('File successfully processed!', event.body);
                    console.log(JSON.stringify(event.body));
                    this.fileResponse = event.body as FileResponse;
                    if (this.fileResponse.errorMessages.length > 0) {
                      var err = ``;
                      for (var i = 0; i < this.fileResponse.errorMessages.length; i++) {
                        err += `Transaction Code: ${this.fileResponse.errorMessages[i].transactionCode}, \n`;
                        err += `lineNumber: ${this.fileResponse.errorMessages[i].lineNumber}, \n`;
                        err += `Errors: \n`;
                        for (var x = 0; x < this.fileResponse.errorMessages[i].errors.errors.length; x++) {
                          err += `Error(${x+1} fieldName): ${this.fileResponse.errorMessages[i].errors.errors[x].fieldName}, \n`;
                          err += `Error(${x+1} message): ${this.fileResponse.errorMessages[i].errors.errors[x].message}\n`;
                        }
                      }
                      this.message = `completed with errors :  (${this.fileResponse.errorMessages.length})\n` +
                        `filename: ${this.fileResponse.fileName} and totalLinesRead: ${this.fileResponse.totalLinesRead}, \n` +
                        `Errors: ` + err;
                    }
                    else {
                      this.message = `completed with (${this.fileResponse.errorMessages.length}) errors: \n` +
                        `filename: ${this.fileResponse.fileName}, totalLinesRead: ${this.fileResponse.totalLinesRead}`;
                    }
                    setTimeout(() => {
                      this.progress = 0;
                    }, 1500);
                }
            })
    }
  }
}
