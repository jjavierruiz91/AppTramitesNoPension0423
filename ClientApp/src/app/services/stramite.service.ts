import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of, observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Stramite } from '../models/stramite';
import { environment } from 'src/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable({
  providedIn: 'root'
})
export class StramiteService {

  constructor(private http: HttpClient) { }

  addStramite(stramite: Stramite): Observable<Stramite> {
    return this.http.post<Stramite>(environment.api_url + 'api/Stramite', stramite, httpOptions).pipe(
      // tap((newStramite: Stramite) => this.log(`nueva solicitud id=${newStramite.codstramite}`)),
      catchError(this.handleError<Stramite>('addStramite'))
    );
  }

  getCodigoStramiteLocalStore(): number {
    return Number(localStorage.getItem('codigosolicitud'));
  }

  getStramite(): Observable<Stramite[]> {
    return this.http.get<Stramite[]>(environment.api_url + 'api/Stramite')
      .pipe(
        // tap(_ => this.log("Lista Cargada")),
        catchError(this.handleError<Stramite[]>('getStramite', []))
      );
  }

  /*getStramiteSectoriales(tramiteId: string ): Observable<Stramite[]> {
    let params = new HttpParams().set('tramiteId', tramiteId);
    return this.http.get<Stramite[]>(environment.api_url + 'api/Stramite/GetTramiteDeporte',{params: params})
      .pipe(
        // tap(_ => this.log("Lista Cargada")),
        catchError(this.handleError<Stramite[]>('getStramite/GetTramiteDeporte', []))
      );
  }*/
  getStramitelist(): Observable<Stramite[]> {
    return this.http.get<Stramite[]>(environment.api_url + 'api/Stramite')
      .pipe(
        catchError(this.handleError<Stramite[]>('getStramite', []))
      );
  }
  get(id: string): Observable<Stramite> {
    const url = `${environment.api_url + 'api/Stramite'}/${id}`;
    return this.http.get<Stramite>(url).pipe(
    );
  }

  update(stramite: Stramite): Observable<any> {
    const url =
      `${environment.api_url + 'api/Stramite'}/${stramite.codstramite}`;
    return this.http.put(url, stramite, httpOptions).pipe(
      // tap(_ => this.log(`updated stramite isbn=${stramite.codstramite}`)),
      catchError(this.handleError<any>('stramite'))
    );
  }

  delete(stramite: Stramite | string): Observable<Stramite> {
    const id = typeof stramite === 'string' ? stramite : stramite.codstramite;
    const url =

      `${environment.api_url + 'api/Stramite'}/${id}`;

    return this.http.delete<Stramite>(url, httpOptions).pipe(
      // tap(_ => this.log(`deleted stramite isbn=${id}`)),
      catchError(this.handleError<Stramite>('deleteTask'))
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
    alert(`ServicioStramite: ${message}`);
  }
}
