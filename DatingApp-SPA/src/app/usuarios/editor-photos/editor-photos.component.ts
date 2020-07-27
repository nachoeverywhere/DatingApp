import { Component, Input, OnInit } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-editor-photos',
  templateUrl: './editor-photos.component.html',
  styleUrls: ['./editor-photos.component.css']
})
export class EditorPhotosComponent implements OnInit {
@Input() photos: Photo[];
// Esto es propio de ng2-file-upload https://valor-software.com/ng2-file-upload/
uploader: FileUploader;
hasBaseDropZoneOver = false;
baseUrl = environment.apiUrl;

constructor(private authService: AuthService) { }

  ngOnInit() {
    this.uploader = new FileUploader({

      url: this.baseUrl + 'usuarios/' + this.authService.tokenDesencriptado.nameid + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('DTApp-token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,

    });


    this.uploader.onAfterAddingFile = (file) => {file.withCredentials = false; } ;
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }




}
