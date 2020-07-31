import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Usuario } from '../_models/usuario';

// Header personalizado
// const httpOptions = {
//   headers: new HttpHeaders({
//     Authorization: 'Bearer ' + localStorage.getItem('DTApp-token')
//   })
// }

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
baseUrl = environment.apiUrl;
// pude haberla puesto a mano pero para ser elegantes la llamo de la carpeta enviroment
constructor(private http: HttpClient) {}

getUsuarios(): Observable<Usuario[]>{

  return this.http.get<Usuario[]>(this.baseUrl + 'usuarios/');
  // le puedo agregar httpOptions para asi enviar el header personalizado como parametro.
  // Al ser un tipo personalizado y no objeto generico tengo que castearlo a array de usuarios
}

getUsuario(id: number): Observable<Usuario>{

  return this.http.get<Usuario>(this.baseUrl + 'usuarios/' + id);
  // Al ser un tipo personalizado y no objeto generico tengo que castearlo a usuario
}

editarUsuario(id: number, usuario: Usuario){
  //                   Va por url                     Va por el body.
 return this.http.put(this.baseUrl + 'usuairos/' + id, usuario);

}

establecerPhotoPrincipal(id: number, idPhoto: number){
  return this.http.post(this.baseUrl + 'usuarios/' + id + '/photos/' + idPhoto + '/establecerPhotoPrincipal', {});
}

}
