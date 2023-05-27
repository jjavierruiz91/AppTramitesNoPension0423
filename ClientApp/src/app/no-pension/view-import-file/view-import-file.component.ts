import { Component, OnInit } from '@angular/core';
import { NopensionService } from 'src/app/services/nopension.service';

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

  constructor(private noPensionService: NopensionService) { }

  ngOnInit() {
    this.onEventLoadPension();
  }

  onEventLoadPension() {
    this.noPensionService.getPensionAll().subscribe({
      next: (value) => {
        console.log('value', value);
        this.pension = value.records;
      },
    })
  }

}
