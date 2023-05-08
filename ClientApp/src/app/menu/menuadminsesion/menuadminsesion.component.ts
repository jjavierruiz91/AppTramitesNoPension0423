import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UsuarioService } from 'src/app/services/usuario.service';
import { Usuario } from 'src/app/models/usuario';


@Component({
  selector: 'app-menuadminsesion',
  templateUrl: './menuadminsesion.component.html',
  styleUrls: ['./menuadminsesion.component.css']
})
export class MenuadminsesionComponent implements OnInit {

  constructor(private router: Router, private servicios: AuthService,private usuarioService: UsuarioService) { }

  nom: string;
  apelli: string;
  rol: string;
  correo: string;
  espacio: string;
  usuario: Usuario;

  ngOnInit() {
    this.getAll();
    this.espacio=" ";
    this.usuarioService.get(this.servicios.getCodigoUserLocalStore()).subscribe(data => this.usuario = data);

  }

  onLogout() {
    localStorage.removeItem('nombres');
    localStorage.removeItem('token');
    localStorage.removeItem('apellidos');
    localStorage.removeItem('email');
    localStorage.removeItem('rol');
    localStorage.removeItem('codigosolicitud');
    localStorage.removeItem('codigoU');
    this.router.navigate(['Ingresar']);
  }
  CambiarCLave() {
    this.router.navigate(['CambiarClave/' + this.servicios.getTokenLocalStore() + '/'+this.usuario.correo]);
  }

  DatosPersonalesAdministrador() {
    this.router.navigate(['DatosPersonalesAdmin']);
  }

  getAll() {
    this.nom=this.servicios.getNombreLocalStore();
    this.apelli=this.servicios.getApellidoLocalStore();
    this.rol=this.servicios.getRolLocalStore();
    this.correo=this.servicios.getCorreoLocalStore();
  }
}
