import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuFuncionarioComponent } from './menu-funcionario/menu-funcionario.component';


@NgModule({
  declarations: [MenuFuncionarioComponent],
  imports: [
    CommonModule,
  ],
  exports: [MenuFuncionarioComponent]
})
export class MenuModule { }
