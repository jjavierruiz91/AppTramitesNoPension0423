import { Component, OnInit } from '@angular/core';
import { NopensionService } from 'src/app/services/nopension.service';

@Component({
  selector: 'app-view-import-file',
  templateUrl: './view-import-file.component.html',
  styleUrls: ['./view-import-file.component.css']
})
export class ViewImportFileComponent implements OnInit {
  pension = []
  pagination: any;

  page = 1;
  pageSize = 10;
  collectionSize = 300;
  currentPage = 1;
  files: any[] = [];
  constructor(private noPensionService: NopensionService) { }

  ngOnInit() {
    this.onEventLoadPension();
  }

  onEventLoadPension(page: number = 1) {
    this.noPensionService.getPensionAll(page).subscribe({
      next: (value) => {
        this.collectionSize = value.total_records;
        this.pension = value.records;
      },
    })
  }

  onEventFIleSelected(e: any) {
    this.files.push(e.target.files[0]);
    this.onEventLoadFile();
  }

  onEventLoadFile() {
    const formData = new FormData();

    for (let index = 0; index < this.files.length; index++) {
      formData.append("Archive", this.files[index]);
    }

    this.noPensionService.postLoadArchives(formData).subscribe({
      next: (value) => {
        console.log(value);
        this.noPensionService.showMessageSuccess("Se cargo correctamente el archivo", "Enorabuena!")
      },
      error: (err) => {
        this.noPensionService.showMessageError("Ocurrio un error al cargar el documento contactate con el administrador", "Error!")
      },
    })
  }

  onEventRefreshData() {
    this.onEventLoadPension(this.page);
  }

}
