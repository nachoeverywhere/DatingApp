import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import {JwtHelperService} from '@auth0/angular-jwt';

// Al ser el componente principal todas las variables seran accesibles o se cargaran primero dependiendo del uso.
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Nachindr';
  jwtHelper = new JwtHelperService();
  constructor(private authService: AuthService){}

  ngOnInit(): void {
    const token = localStorage.getItem('DTApp-token');
    if (token){
      this.authService.tokenDesencriptado = this.jwtHelper.decodeToken(token);
    }
  }


}
