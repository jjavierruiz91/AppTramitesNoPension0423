import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ViewValidateQrComponent } from './view-validate-qr/view-validate-qr.component';


const routes: Routes = [
  { path: 'validate', component: ViewValidateQrComponent },
  { path: '**', redirectTo: 'validate' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule { }
