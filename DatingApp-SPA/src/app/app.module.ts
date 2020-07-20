import {BsDropdownModule} from 'node_modules/ngx-bootstrap/dropdown';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { JwtModule } from '@auth0/angular-jwt';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import {RouterModule} from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TabsModule } from 'ngx-bootstrap/tabs';
import {NgxGalleryModule} from '@kolkov/ngx-gallery';

import { NavComponent } from './nav/nav.component';
import { AppComponent } from './app.component';
import { AuthService } from './_services/auth.service';
import { RegistroComponent } from './registro/registro.component';
import { InicioComponent } from './inicio/inicio.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { ListaUsuariosComponent } from './usuarios/lista-usuarios/lista-usuarios.component';
import { TarjetaUsuariosComponent } from './usuarios/tarjeta-usuarios/tarjeta-usuarios.component';
import { ListaMatchesComponent } from './lista-matches/lista-matches.component';
import { MensajesComponent } from './mensajes/mensajes.component';
import { appRoutes } from './routes';
import { DetalleUsuariosComponent } from './usuarios/detalle-usuarios/detalle-usuarios.component';
import { DetalleUsuariosResolver } from '../app/_resolvers/detalle-usuarios.resolver';
import { ListaUsuariosResolver } from '../app/_resolvers/lista-usuarios.resolver';

export function obtenerToken() {
   return localStorage.getItem('DTApp-token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      RegistroComponent,
      InicioComponent,
      ListaUsuariosComponent,
      ListaMatchesComponent,
      MensajesComponent,
      TarjetaUsuariosComponent,
      DetalleUsuariosComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BrowserAnimationsModule,
      TabsModule.forRoot(),
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      NgxGalleryModule,
      JwtModule.forRoot({
         config: {
            tokenGetter: obtenerToken,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      })
   ],
   providers: [
      AuthService,
      DetalleUsuariosResolver,
      ListaUsuariosResolver,
      ErrorInterceptorProvider
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
