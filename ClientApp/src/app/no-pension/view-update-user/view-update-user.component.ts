import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserNoPension } from 'src/app/models/usuario';
import { NopensionService } from 'src/app/services/nopension.service';

@Component({
  selector: 'app-view-update-user',
  templateUrl: './view-update-user.component.html',
  styleUrls: ['./view-update-user.component.css']
})
export class ViewUpdateUserComponent implements OnInit {
  user: UserNoPension = { codsolicitante: 0, identificacion: "A", nombrecompleto: "", estado: "" };
  selectStatus: string = "";

  constructor(private routerActive: ActivatedRoute, private noPension: NopensionService, private router: Router) {
    this.onEventGetIdentificationByUrl();
  }

  ngOnInit() {
  }

  onEventGetIdentificationByUrl() {
    this.routerActive.params.subscribe({
      next: (value) => {
        this.onEventGetUserByIdentification(value.id)
      },
    })
  }

  onEventGetUserByIdentification(identification: string) {
    this.noPension.getUserByIdentification(identification).subscribe({
      next: (value: UserNoPension) => {
        this.user = value;
      },
    })
  }

  onEventUpdateUser() {
    console.log(this.user);
    this.noPension.putUpdateUserNoPension(this.user).subscribe({
      next: (value) => {
        this.noPension.showMessageSuccess("Informacion actualizada", "Enorabuena!");
        this.router.navigate(["/pension"])
      },
      error: (err) => {
        this.noPension.showMessageError("Se produjo un error al actualizar el usuario", "Error!")

      },
    })
  }
}
