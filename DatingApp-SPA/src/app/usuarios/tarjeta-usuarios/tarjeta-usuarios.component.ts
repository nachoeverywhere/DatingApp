import { Component, OnInit, Input } from '@angular/core';
import { Usuario } from 'src/app/_models/usuario';

@Component({
  selector: 'app-tarjeta-usuarios',
  templateUrl: './tarjeta-usuarios.component.html',
  styleUrls: ['./tarjeta-usuarios.component.css']
})
export class TarjetaUsuariosComponent implements OnInit {
  @Input() usuario: Usuario; // Declaro input por que recibe elementos de su clase padre.
  constructor() { }

  ngOnInit() {
  }

}
