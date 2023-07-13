import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  pageSize: number = 10;
  collectionSize = 0;
  currentPage = 1;
  files: any[] = [];
  constructor(private noPensionService: NopensionService, private router: Router) { }

  ngOnInit() {
    this.onEventLoadPension();
  }

  onEventLoadPension(page: number = 1) {
    this.noPensionService.getPensionAll(page).subscribe({
      next: (value) => {
        this.collectionSize = value.total_records;
        this.pageSize = value.records.length;
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
        this.noPensionService.showMessageSuccess("Se cargo correctamente el archivo", "Enorabuena!");
        this.onEventLoadPension();
      },
      error: (err) => {
        let message: string = "Ocurrio un error al cargar el documento contactate con el administrador";
        if (err.error.statusCode === 502 || err.error.statusCode === 405) {
          message = err.error.message;
        }

        this.noPensionService.showMessageError(message, "Error!")
      },
    })
  }

  onEventRefreshData() {
    this.onEventLoadPension(this.page);
  }

  onEventUpdateUser(userId: string) {
    this.router.navigate(["pension/update", userId])
  }

}
