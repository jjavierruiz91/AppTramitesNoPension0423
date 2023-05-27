import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NoPensionRoutingModule } from './no-pension-routing.module';
import { ViewImportFileComponent } from './view-import-file/view-import-file.component';
import { MenusesionComponent } from '../menu/menusesion/menusesion.component';


@NgModule({
  declarations: [ViewImportFileComponent],
  imports: [
    CommonModule,
    NoPensionRoutingModule
  ]
})
export class NoPensionModule { }
