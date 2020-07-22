import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanDeactivate } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import {EditarUsuariosComponent} from '../usuarios/editar-usuarios/editar-usuarios.component';


@Injectable({
    providedIn: 'root'
  })

export class PrevenirCambiosNoGuardados implements CanDeactivate<EditarUsuariosComponent> {
    constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService){}

    canDeactivate(component: EditarUsuariosComponent, currentRoute: ActivatedRouteSnapshot,
                  currentState: RouterStateSnapshot, nextState?: RouterStateSnapshot): boolean
                  | import('@angular/router').UrlTree | import('rxjs').Observable<boolean |
                    import('@angular/router').UrlTree> | Promise<boolean | import('@angular/router').UrlTree> {

        if (component.formulario.dirty){
        return confirm('Tienes cambios sin guardar. Deseas salir de todas formas? \n (Los cambios no guardados se perderan)');
        }
        return true;
    }

  }
