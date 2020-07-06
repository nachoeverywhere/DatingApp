import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent implements OnInit {
  modoRegistro = false;
  // valores: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    // this.getValores();
  }

  sesionActiva(){
    const token = localStorage.getItem('token');
    return !!token;
  }

  disparadorRegistro(){
    this.modoRegistro = true;
  }

  // Enviar valores a clase hijo
  // getValores(){
  //   this.http.get('http://localhost:5000/api/valores').subscribe(response => {
  //   this.valores = response;
  //   }, error => {
  //     console.log(error);
  //   });
  // }

  cancelarModoRegistro(modoRegistro: boolean){
    this.modoRegistro = modoRegistro;
  }

}
