import { Component, OnInit } from '@angular/core';
import { Usuario } from 'src/app/models/usuario';
import { AuthService } from 'src/app/services/auth.service';
import { UsuarioService } from 'src/app/services/usuario.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-datos-personales-admin',
  templateUrl: './datos-personales-admin.component.html',
  styleUrls: ['./datos-personales-admin.component.css']
})
export class DatosPersonalesAdminComponent implements OnInit {
  codusuario: number;
  usuario: Usuario;

  constructor(private authService: AuthService, private usuarioService: UsuarioService, private location: Location) { }


  ngOnInit() {
    this.codusuario = this.authService.getCodigoUserLocalStore();
    this.usuarioService.get(this.codusuario).subscribe(data => this.usuario = data);
  }
  cancelar() {
    this.goBack();
  }
  goBack(): void {
    this.location.back();
  }
  


}
