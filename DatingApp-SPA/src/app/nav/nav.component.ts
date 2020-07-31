import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit(): void {
  }

  ingresar(): void{
    this.authService.ingresar(this.model).subscribe(next => {
      this.alertify.exito('Exito');
      // Podria redireccionar directamente desde aqui. Pero para probar lo hago mas abajo.
    }, error => {
      this.alertify.error(error);
    }, () => {
      this.router.navigate(['/usuarios']);
    }
    );
  }

  sesionActiva(): any{
    return this.authService.sessionActiva();
  }


  salir(): void{
    localStorage.removeItem('DTApp-token');
    localStorage.removeItem('DTApp-usuario');
    this.authService.tokenDesencriptado = null;
    this.authService.usuarioActivo = null;
    this.alertify.mensaje('Sesion cerrada');
    this.router.navigate(['/inicio']);
  }
}
