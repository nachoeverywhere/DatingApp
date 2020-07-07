// uso de la extension alertify no es propio de angular.
import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }
  // Como parametro recibo un string y una funcion (que no requiere parametros)(Callback) :void es el return type.
  confirmar(mensaje: string, funcionCallback: () => any): void {
    alertify.confirm(mensaje, (evento: any) => {
      if (evento) {
        funcionCallback();
      } else {}
    });
  }

  exito(mensaje: string): void {
    alertify.success(mensaje);
  }

  error(mensaje: string): void {
    alertify.error(mensaje);
  }

  advertencia(mensaje: string): void {
    alertify.warning(mensaje);
  }

  mensaje(mensaje: string): void {
    alertify.message(mensaje);
  }
}

