import {Routes} from '@angular/router';
import {InicioComponent} from './inicio/inicio.component';
import {ListaUsuariosComponent} from './usuarios/lista-usuarios/lista-usuarios.component';
import {ListaMatchesComponent} from './lista-matches/lista-matches.component';
import { DetalleUsuariosComponent } from './usuarios/detalle-usuarios/detalle-usuarios.component';
import {MensajesComponent} from './mensajes/mensajes.component';
import {AuthGuard} from './_guards/auth.guard';
import { DetalleUsuariosResolver } from '../app/_resolvers/detalle-usuarios.resolver';
import { ListaUsuariosResolver } from '../app/_resolvers/lista-usuarios.resolver';
import { EditarUsuariosResolver } from '../app/_resolvers/editar-usuarios.resolver';
import { EditarUsuariosComponent } from './usuarios/editar-usuarios/editar-usuarios.component';
import { PrevenirCambiosNoGuardados } from './_guards/prevenir-cambios-no-guardados.guard';


// Luego de declarar las rutas aqui debo declararlas en app module y recibirlas a traves del metodo RoutesModule.

export const appRoutes: Routes = [
    {path: '', component: InicioComponent},
    {path: '', // OJO QUE CON ESTO limito el acceso a cualquier otra path que no sea INICIO;
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
        {path: 'usuarios', component: ListaUsuariosComponent, resolve: {usuarios: ListaUsuariosResolver}},
        {path: 'usuarios/:id', component: DetalleUsuariosComponent, resolve: {usuario: DetalleUsuariosResolver}},
        {path: 'usuario/editar', component: EditarUsuariosComponent, resolve: {usuario: EditarUsuariosResolver}, 
        canDeactivate:[PrevenirCambiosNoGuardados]},
        {path: 'mensajes', component: MensajesComponent},
        {path: 'matches', component: ListaMatchesComponent},
    ]
    },
    // el can activate chequea la condicion del authguard antes de continuar. Lo podria poner individualmente.
    // {path: 'matches', component: ListaMatchesComponent, canActivate: [AuthGuard]},
    {path: '**', redirectTo: '', component: InicioComponent}
];
