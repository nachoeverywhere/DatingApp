import{Injectable} from '@angular/core';
import{Usuario} from '../_models/usuario';
import{Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import{UsuarioService} from '../_services/usuario.service';
import{AlertifyService} from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class DetalleUsuariosResolver implements Resolve<Usuario>{
    constructor(private usuarioService: UsuarioService, private router: Router, private alertify: AlertifyService){}

    // Cuando se usa un resolve, automaticamente hace 'suscribe' al metodo, por eso no lo hago aqui.
    resolve(route: ActivatedRouteSnapshot): Observable<Usuario>{
        return this.usuarioService.getUsuario(+route.params['id']).pipe(
            catchError(error => {
                this.alertify.error('Ha ocurrido un problema obteniendo los datos del usuario');
                this.router.navigate(['/members']);
                return of(null);
            })
        );
    }
}
