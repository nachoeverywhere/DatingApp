import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AuthService } from './_services/auth.service';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import {BsDropdownModule} from 'node_modules/ngx-bootstrap/dropdown';
import {RouterModule} from '@angular/router';

import { RegistroComponent } from './registro/registro.component';
import { InicioComponent } from './inicio/inicio.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ListaUsuariosComponent } from './lista-usuarios/lista-usuarios.component';
import { ListaMatchesComponent } from './lista-matches/lista-matches.component';
import { MensajesComponent } from './mensajes/mensajes.component';
import { appRoutes } from './routes';
@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      RegistroComponent,
      InicioComponent,
      ListaUsuariosComponent,
      ListaMatchesComponent,
      MensajesComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
