import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Patent, CreatePatent, UpdatePatent } from '../models/patent.model';

@Injectable({
  providedIn: 'root'
})
export class PatentService {
  private apiUrl = `${environment.apiUrl}/patents`;

  constructor(private http: HttpClient) {}

  getAll(status?: string, clientId?: number, pageNumber = 1, pageSize = 10): Observable<any> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());
    
    if (status) params = params.set('status', status);
    if (clientId) params = params.set('clientId', clientId.toString());

    return this.http.get<any>(this.apiUrl, { params });
  }

  getById(id: number): Observable<Patent> {
    return this.http.get<Patent>(`${this.apiUrl}/${id}`);
  }

  create(patent: CreatePatent): Observable<Patent> {
    return this.http.post<Patent>(this.apiUrl, patent);
  }

  update(id: number, patent: UpdatePatent): Observable<Patent> {
    return this.http.put<Patent>(`${this.apiUrl}/${id}`, patent);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
