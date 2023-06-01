import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NoPensionRoutingModule } from './no-pension-routing.module';
import { ViewImportFileComponent } from './view-import-file/view-import-file.component';
import { NgbPaginationModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MenuModule } from '../menu/menu.module';
import { ViewUpdateUserComponent } from './view-update-user/view-update-user.component';


@NgModule({
  declarations: [ViewImportFileComponent, ViewUpdateUserComponent],
  imports: [
    CommonModule,
    NoPensionRoutingModule,
    NgbTypeaheadModule,
    NgbPaginationModule,
    FormsModule,
    ReactiveFormsModule,
    MenuModule,
  ]
})
export class NoPensionModule { }
