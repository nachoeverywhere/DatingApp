import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent implements OnInit {
  @Input() valoresDeInicio: any;
  @Output() cancelarRegistro = new EventEmitter();

  model: any = {};

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }



  registro(){
    this.authService.registrar(this.model).subscribe( () => {
    console.log('Exito');
    }, error => {console.log(error); } );
  }

  cancelar(){
    this.cancelarRegistro.emit(false);
  }

}
