import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-valor',
  templateUrl: './valor.component.html',
  styleUrls: ['./valor.component.css']
})
export class ValorComponent implements OnInit {
  valores: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getValores();
  }

  getValores(){
    this.http.get('http://localhost:5000/api/valores').subscribe(response => {
    this.valores = response;
    }, error => {
      console.log(error);
    });
  }

}
