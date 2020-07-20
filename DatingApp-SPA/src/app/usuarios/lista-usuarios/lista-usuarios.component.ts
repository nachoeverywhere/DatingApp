import { Component, OnInit } from '@angular/core';
import { Usuario } from '../../_models/usuario';
import { UsuarioService } from '../../_services/usuario.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-lista-usuarios',
  templateUrl: './lista-usuarios.component.html',
  styleUrls: ['./lista-usuarios.component.css']
})
export class ListaUsuariosComponent implements OnInit {
  usuarios: Usuario[];
  constructor(private usuarioServicio: UsuarioService, private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {

    this.route.data.subscribe(data => {
      // el nombre entre los corchetes rectos corresponde al nombre del parametro le asigne en routes.
      this.usuarios = data['usuarios'];
    });

  // this.cargarUsuarios(); -- Lo saco ya que ahora hago uso de un route resolver.


  }

  // cargarUsuarios(){
  //   this.usuarioServicio.getUsuarios().subscribe((usuarios: Usuario[]) => {
  //     this.usuarios = usuarios;
  //   }, error => {this.alertify.error(error);
  //   });
  // }
}
