import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  ingresar(){
    this.authService.ingresar(this.model).subscribe(next => {
      console.log('Exito');
    }, error => {
      console.log(error);
    });
  }

  sesionActiva(){
    const token = localStorage.getItem('token');
    return !!token;
  }


  salir(){
    localStorage.removeItem('token');
    console.log('Sesion cerrada');
  }
}
