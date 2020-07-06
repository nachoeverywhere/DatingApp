import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>, // La respuesta misma.
    next: HttpHandler // Indica que ocurre despues de recibirla. Es el handler, el que la trata y/o procesa.
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
        catchError(error => {
            if (error.status === 401) { // en vez de ser el status puedo controlar algun otro atributo del json devuelve la api.
                return throwError(error.statusText);
            }
            if (error instanceof HttpErrorResponse){
                // Los application errors corresponden a los 500+ errors(Errores de servidor,vienen en los headers)
                // En este caso capturo el header Application-Error que lo declare en la API. Puede ser cualquier otro.
                const applicationError = error.headers.get('Application-Error');
                if (applicationError){
                    return throwError(applicationError);
                }
                 // Como llame al error generarl error y quiero acceder a la propiedad JSON 'error' me queda asi:
                const serverError = error.error;
                let modalStateErrors = '';
                if (serverError.errors && typeof serverError.errors === 'object') {
                    for (const key in serverError.errors){
                        if (serverError.errors[key]){ // Object-Bracket notation, la key hace referencia al nombre del objeto.
                            modalStateErrors += serverError.errors[key] + '\n';
                        }
                    }
                } // Chequeo si existe un array de errores (ej: 'usuario incorrecto', 'campo requerido', etc).
                 // Controlo si es objeto para saber si utilizar ese tipo de metodos.
                // (Tal vez pueda recibier un o jeto personalizado queseyo).
                return throwError(modalStateErrors || serverError || 'Error de servidor desconocido');
                // Si tengo errores en modal, lo devuelvo, en caso que sea un empty string devuelvo
                // ServerError, y en caso ese tambien sea null devuelvo 'Error de servidor generico'.
            }

        })
    );
  }
}

// Estoy agregando el metodo como un nuevo provider al array de providers que ya existe en angular.
// Luego hay que especificarlo en app module.
export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
