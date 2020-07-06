// Cuando se agrega un servicio hay que declararlo en components dentro de app.module
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';


// Injectable significa que puede ser injectado en componentes.
@Injectable({
  providedIn: 'root'
})

export class AuthService {
baseUrl = 'http://localhost:5000/api/auth/';

constructor(private http: HttpClient) {}

ingresar(model: any){
  return this.http.post(this.baseUrl + 'ingresar', model).pipe
  (
    map((response: any) => {
      const usuario = response;
      if (usuario){
        localStorage.setItem('token', usuario.token);
      }
    })
  );
}

 // Luego de enviar la solicitud agarra con .pipe la respuesta
 // y mapea a una variable temporal llamada usuario, ya que la respuesta es un JSON
 // Puede acceder a sus atributos con '.' , en este caso nos quedamos ocn el token.

 registrar(model: any){
   return this.http.post(this.baseUrl + 'registrar', model);
 }

}
