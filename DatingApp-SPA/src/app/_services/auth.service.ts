// Cuando se agrega un servicio hay que declararlo en components dentro de app.module
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';
import { Usuario } from '../_models/usuario';

// Injectable significa que puede ser injectado en componentes.
@Injectable({
  providedIn: 'root'
})

export class AuthService {
baseUrl = environment.apiUrl + 'auth/';
jwtHelper = new JwtHelperService();
tokenDesencriptado: any;
usuarioActivo: Usuario;

constructor(private http: HttpClient) {}

ingresar(model: any): any{
  return this.http.post(this.baseUrl + 'ingresar', model).pipe
  (
    map((response: any) => {
      const usuario = response;
      if (usuario){
        localStorage.setItem('DTApp-token', usuario.token);
        localStorage.setItem('DTApp-usuario', JSON.stringify(usuario));
        this.usuarioActivo = usuario.usuarioRespuesta;
        this.tokenDesencriptado = this.jwtHelper.decodeToken(usuario.token);
      }
    })
  );
}

 // Luego de enviar la solicitud agarra con .pipe la respuesta
 // y mapea a una variable temporal llamada usuario, ya que la respuesta es un JSON
 // Puede acceder a sus atributos con '.' , en este caso nos quedamos ocn el token.

 registrar(model: any): any{
   return this.http.post(this.baseUrl + 'registrar', model);
 }

 sessionActiva(): any{
   const token = localStorage.getItem('DTApp-token');
   return !this.jwtHelper.isTokenExpired(token);
   // Le pongo ! ya que indicaria que existe un token y que este no esta expirado -> esta logueado.
 }

}
