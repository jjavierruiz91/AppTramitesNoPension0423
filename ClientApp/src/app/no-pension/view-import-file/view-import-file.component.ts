import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-view-import-file',
  templateUrl: './view-import-file.component.html',
  styleUrls: ['./view-import-file.component.css']
})
export class ViewImportFileComponent implements OnInit {
  pension = [
    { identification: 1122222, nombrecompleto: 'Andres', estado: 'pensionado' },
    { identification: 1122222, nombrecompleto: 'Andres', estado: 'pensionado' },
    { identification: 1122222, nombrecompleto: 'Andres', estado: 'pensionado' },
    { identification: 1122222, nombrecompleto: 'Andres', estado: 'pensionado' },
    { identification: 1122222, nombrecompleto: 'Andres', estado: 'pensionado' },
    { identification: 1122222, nombrecompleto: 'Andres', estado: 'jubilado' },
    { identification: 1122222, nombrecompleto: 'Andres', estado: 'jubilado' },
    { identification: 1122222, nombrecompleto: 'Andres', estado: 'jubilado' },
    { identification: 1122222, nombrecompleto: 'Andres', estado: 'jubilado' },
    { identification: 1122222, nombrecompleto: 'Andres', estado: 'pensionado' },
    { identification: 1122222, nombrecompleto: 'Andres', estado: 'pensionado' },
    { identification: 1122222, nombrecompleto: 'Andres', estado: 'pensionado' }

  ]

  constructor() { }

  ngOnInit() {
    this.onEventLoadPension();
  }

  onEventLoadPension(){
    
  }

}
