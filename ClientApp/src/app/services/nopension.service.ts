import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of, observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Nopension } from '../models/nopension';
import { environment } from 'src/environments/environment';
import { ToastrService } from 'ngx-toastr';
import { UserNoPension } from '../models/usuario';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable({
  providedIn: 'root'
})
export class NopensionService {

  constructor(private http: HttpClient, public toastr: ToastrService) { }

  get(id: string): Observable<any> {
    const url = `${environment.api_url + 'api/Nopension/certificado-nopension'}/${id}`;
    return this.http.get(url, { responseType: "blob" })
  }

  getUserByIdentification(identification: string): Observable<UserNoPension> {
    const url = `${environment.api_url + 'api/Nopension'}/${identification}`;
    return this.http.get<UserNoPension>(url)
  }

  getPensionAll(page: number = 1): Observable<any> {
    const params = new HttpParams().set('page', page.toString());

    const url = `${environment.api_url + 'api/Nopension'}`;
    return this.http.get(url, { params })
  }

  postLoadArchives(Archive: FormData): Observable<any> {
    const url = `${environment.api_url + 'api/Nopension'}`;
    return this.http.post(url, Archive)
  }

  putUpdateUserNoPension(payload: UserNoPension): Observable<string> {
    const url = `${environment.api_url + 'api/Nopension'}`;
    return this.http.post<string>(url, payload);
  }

  showMessageSuccess(message: string, title: string) {
    this.toastr.success(message, title);
  }


  showMessageError(message: string, title: string) {
    this.toastr.error(message, title);
  }
}

