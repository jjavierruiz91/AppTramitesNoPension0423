import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Nopension, PublicNoPension } from 'src/app/models/nopension';
import { NopensionService } from 'src/app/services/nopension.service';

@Component({
  selector: 'app-view-validate-qr',
  templateUrl: './view-validate-qr.component.html',
  styleUrls: ['./view-validate-qr.component.css']
})
export class ViewValidateQrComponent implements OnInit {
  noPension: PublicNoPension

  constructor(private noPensionService: NopensionService, private routerActive: ActivatedRoute) {
    this.routerActive.queryParamMap.subscribe(param => this.getNoPensionByToken(param.get("token")))

  }

  ngOnInit() {
  }

  getNoPensionByToken(token: string) {
    this.noPensionService.getNoPensionByToken(token).subscribe(pension => {
      this.noPension = pension;
    })
  }

}
