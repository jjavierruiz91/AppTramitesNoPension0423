import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NopensionService } from 'src/app/services/nopension.service';

@Component({
  selector: 'app-view-upload-file',
  templateUrl: './view-upload-file.component.html',
  styleUrls: ['./view-upload-file.component.css']
})
export class ViewUploadFileComponent implements OnInit {
  files: any[] = [];
  constructor(private noPensionService: NopensionService, private router: Router) { }

  ngOnInit() {
  }

  onEventFIleSelected(e: any) {
    this.files.push(e.target.files[0]);
  }

  onEventLoadFile() {
    const formData = new FormData();

    for (let index = 0; index < this.files.length; index++) {
      formData.append("Archive", this.files[index]);
    }

    this.noPensionService.postLoadArchives(formData).subscribe({
      next: (value) => {
        this.noPensionService.showMessageSuccess("Se cargo correctamente el archivo", "Enorabuena!");
        this.onEventViewPension()
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

  onEventViewPension() {
    this.router.navigate(["pension",])
  }
}
