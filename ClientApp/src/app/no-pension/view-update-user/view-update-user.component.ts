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

  constructor(private router: ActivatedRoute, private noPension: NopensionService) {
    this.onEventGetIdentificationByUrl();
  }

  ngOnInit() {
  }

  onEventGetIdentificationByUrl() {
    this.router.params.subscribe({
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

  }
}
