import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserRoles } from '../models/usuario';

@Injectable({
  providedIn: 'root'
})
export class AuthadminGuard implements CanActivate {
  constructor(private router: Router) {
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    const rol = localStorage.getItem('rol');
    if (localStorage.getItem('token') != null && rol == UserRoles.Admin || rol === UserRoles.FuncionarioNoPension)
      return true;
    else {
      this.router.navigate(['Ingresar']);
      return false;
    }
  }

}
