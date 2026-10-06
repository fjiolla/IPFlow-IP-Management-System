import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Trademark, CreateTrademark, UpdateTrademark } from '../models/trademark.model';

@Injectable({
  providedIn: 'root'
})
export class TrademarkService {
  private apiUrl = `${environment.apiUrl}/trademarks`;

  constructor(private http: HttpClient) {}

  getAll(status?: string, clientId?: number, pageNumber = 1, pageSize = 10): Observable<any> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());
    
    if (status) params = params.set('status', status);
    if (clientId) params = params.set('clientId', clientId.toString());

    return this.http.get<any>(this.apiUrl, { params });
  }

  getById(id: number): Observable<Trademark> {
    return this.http.get<Trademark>(`${this.apiUrl}/${id}`);
  }

  create(trademark: CreateTrademark): Observable<Trademark> {
    return this.http.post<Trademark>(this.apiUrl, trademark);
  }

  update(id: number, trademark: UpdateTrademark): Observable<Trademark> {
    return this.http.put<Trademark>(`${this.apiUrl}/${id}`, trademark);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
