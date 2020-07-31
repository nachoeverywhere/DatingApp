import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { Usuario } from 'src/app/_models/usuario';
import { ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UsuarioService } from 'src/app/_services/usuario.service';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-editar-usuarios',
  templateUrl: './editar-usuarios.component.html',
  styleUrls: ['./editar-usuarios.component.css'],
})
export class EditarUsuariosComponent implements OnInit {
  @ViewChild('formularioTocado', { static: true }) formulario: NgForm; // Con esto puedo acceder a un elemento HTML de la vista.
  usuario: Usuario;
  // Confirmar salida al cerrar la pestana
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.formulario.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private route: ActivatedRoute,
    private alertify: AlertifyService,
    private usuarioService: UsuarioService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.usuario = data['usuario'];
    });
  }

  editarUsuario() {
    this.usuarioService
      .editarUsuario(this.authService.tokenDesencriptado.nameId, this.usuario)
      .subscribe(
        (next) => {
          this.alertify.exito('Actualizado!');
          this.formulario.reset(this.usuario); // Recibe como parametro un modelo (opcional)
        },
        (error) => {
          this.alertify.error(error);
        }
      );
  }

  actualizarPhoto(photoUrl){
    this.usuario.photoUrl = photoUrl;
  }
}
