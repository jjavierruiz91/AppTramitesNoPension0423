import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of, observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Usuario } from '../models/usuario';
import { environment } from 'src/environments/environment';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(private http: HttpClient) { }

  addUsuario(usuario: Usuario): Observable<Usuario> {
    return this.http.post<Usuario>(environment.api_url + 'api/Usuario', usuario, httpOptions).pipe(
      // // tap((newUsuario: Usuario) => this.log(`added NewSocio w/ id=${newUsuario.codusuario}`)),
      catchError(this.handleError<Usuario>('addUsuario'))
    );
  }
  getUsuario(): Observable<Usuario[]> {
    return this.http.get<Usuario[]>(environment.api_url + 'api/Usuario')
      .pipe(
        // tap(_ => this.log("Lista Cargada")),
        catchError(this.handleError<Usuario[]>('getUsuario', []))
      );
  }

  get(codusuario: number): Observable<Usuario> {
    const url = `${environment.api_url + 'api/Usuario'}/${codusuario}`;
    return this.http.get<Usuario>(url).pipe(
    );
  }

  update(usuario: Usuario): Observable<any> {
    const url =

      `${environment.api_url + 'api/Usuario'}/${usuario.codusuario}`;
    return this.http.put(url, usuario, httpOptions).pipe(
      // tap(_ => this.log(`updated usuario isbn=${usuario.codusuario}`)),
      catchError(this.handleError<any>('usuario'))
    );
  }

  delete(usuario: Usuario | string): Observable<Usuario> {
    const id = typeof usuario === 'string' ? usuario : usuario.codusuario;
    const url =

      `${environment.api_url + 'api/Usuario'}/${id}`;

    return this.http.delete<Usuario>(url, httpOptions).pipe(
      // tap(_ => this.log(`deleted usuario isbn=${id}`)),
      catchError(this.handleError<Usuario>('deleteTask'))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} Fallo Listado: ${error.message}`);
      return of(result as T);
    };
  }

  private log(message: string) {
    alert(`ServicioUsuario: ${message}`);
  }
}
