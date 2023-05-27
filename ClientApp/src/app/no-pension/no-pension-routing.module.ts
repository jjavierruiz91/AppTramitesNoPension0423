import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ViewImportFileComponent } from './view-import-file/view-import-file.component';


const routes: Routes = [
  { path: '', component: ViewImportFileComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NoPensionRoutingModule { }
