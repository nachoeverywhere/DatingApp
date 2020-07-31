import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
import { UsuarioService } from 'src/app/_services/usuario.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-editor-photos',
  templateUrl: './editor-photos.component.html',
  styleUrls: ['./editor-photos.component.css'],
})
export class EditorPhotosComponent implements OnInit {
  @Input() photos: Photo[];
  //   Output properties emiten eventos, por eso EventEmmitter
  @Output() cambiarPhotoUsuario = new EventEmitter<string>();
  // Esto es propio de ng2-file-upload https://valor-software.com/ng2-file-upload/
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  photoPerfilActual: Photo;

  constructor(
    private authService: AuthService,
    private usuarioService: UsuarioService,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.initializeUploader();
  }

  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url:
        this.baseUrl +
        'usuarios/' +
        this.authService.tokenDesencriptado.nameid +
        '/photos',
      authToken: 'Bearer ' + localStorage.getItem('DTApp-token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.alias = 'Archivo';
      file.withCredentials = false;
    };
    // aqui procesa la respuesta
    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response); // Converting String into an object
        const photo = {
          id: res.id,
          url: res.url,
          fechaSubida: res.fechaSubida,
          descripcion: res.descripcion,
          esPrincipal: res.esPrincipal,
        };
        this.photos.push(photo);
      }
    };
  }

  establecerPrincipal(photo: Photo){
    this.usuarioService.establecerPhotoPrincipal(this.authService.tokenDesencriptado.nameid, photo.id).subscribe(
      () => {
        this.photoPerfilActual = this.photos.filter(p => p.esPrincipal === true)[0];
        this.photoPerfilActual.esPrincipal = false;
        photo.esPrincipal = true;
        this.cambiarPhotoUsuario.emit(photo.url);
        this.alertify.exito('Actualizado!');
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }
}
