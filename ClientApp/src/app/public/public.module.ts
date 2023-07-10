import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicRoutingModule } from './public-routing.module';
import { ViewValidateQrComponent } from './view-validate-qr/view-validate-qr.component';


@NgModule({
  declarations: [ViewValidateQrComponent],
  imports: [
    CommonModule,
    PublicRoutingModule
  ]
})
export class PublicModule { }
