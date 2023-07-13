import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ViewImportFileComponent } from './view-import-file/view-import-file.component';
import { ViewUpdateUserComponent } from './view-update-user/view-update-user.component';
import { ViewUploadFileComponent } from './view-upload-file/view-upload-file.component';


const routes: Routes = [
  { path: '', component: ViewImportFileComponent },
  { path: 'upload', component: ViewUploadFileComponent },
  { path: 'update/:id', component: ViewUpdateUserComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NoPensionRoutingModule { }
