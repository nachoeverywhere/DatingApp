import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {
  @Input() valoresDeInicio: any;
  @Output() cancelarRegistro = new EventEmitter();

  model: any = {};

  constructor(private authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }



  registro(){
    this.authService.registrar(this.model).subscribe( () => {
      this.alertify.exito('Exito');
    }, error => {this.alertify.error(error); } );
  }

  cancelar(){
    this.cancelarRegistro.emit(false);
  }

}
