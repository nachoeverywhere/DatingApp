import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit(): void {
  }

  ingresar(): void{
    this.authService.ingresar(this.model).subscribe(next => {
      this.alertify.exito('Exito');
    }, error => {
      this.alertify.error(error);
    });
  }

  sesionActiva(): any{
    return this.authService.sessionActiva();
  }


  salir(): void{
    localStorage.removeItem('DTApp-token');
    this.alertify.mensaje('Sesion cerrada');
  }
}
