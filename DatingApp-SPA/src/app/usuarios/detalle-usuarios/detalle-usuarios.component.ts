import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/_models/usuario';
import { UsuarioService } from 'src/app/_services/usuario.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryModule, NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation} from '@kolkov/ngx-gallery';


@Component({
  selector: 'app-detalle-usuarios',
  templateUrl: './detalle-usuarios.component.html',
  styleUrls: ['./detalle-usuarios.component.css']
})
export class DetalleUsuariosComponent implements OnInit {
  usuario: Usuario;
  // Propios del componente NgxGallery - Necesarios para su funcionamiento (indicado en la documentacion del componente)
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  // El ActivatedRoute significa que cuando se acceda a la ruta tendremos accesso a ese parametro en particular.
  constructor(private usuarioService: UsuarioService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      // el nombre entre los corchetes rectos corresponde al nombre del parametro le asigne en routes.
      this.usuario = data['usuario'];
    });
    // this.cargarUsuario(); -- Lo saco ya que ahora hago uso de un route resolver.

    // NgxGallery
    this.galleryOptions = [{
      width: '500px',
      height: '500px',
      imagePercent: 100,
      thumbnailsColumns: 4,
      imageAnimation: NgxGalleryAnimation.Slide,
      preview: false
    }];
    this.galleryImages = this.getFotosDeUsuario();
  }

  getFotosDeUsuario(){
    const photosUrls = [];
    for (const photo of this.usuario.fotosPublicas){
      photosUrls.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url,
        description: photo.descripcion
      });
    }
    return photosUrls;
  }

  // usuarios/id
  // cargarUsuario(){
  //   // Obtengo el parametro a traves de la ruta, especifico el nombre del parametro dentro de los corchetes rectos,
  //   // y le indico lo convierta a tipo numericio con el simbolo de + al comienzo de la sentencia.
  //   this.usuarioService.getUsuario(+this.route.snapshot.params['id']).subscribe((u: Usuario) => {
  //     this.usuario = u;
  //   }, error => {
  //     this.alertify.error(error);
  //   });
  // }

}
