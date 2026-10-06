import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Case, CreateCase, UpdateCase } from '../models/case.model';

@Injectable({
  providedIn: 'root'
})
export class CaseService {
  private apiUrl = `${environment.apiUrl}/cases`;

  constructor(private http: HttpClient) {}

  getAll(status?: string, clientId?: number, pageNumber = 1, pageSize = 10): Observable<any> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());
    
    if (status) params = params.set('status', status);
    if (clientId) params = params.set('clientId', clientId.toString());

    return this.http.get<any>(this.apiUrl, { params });
  }

  getById(id: number): Observable<Case> {
    return this.http.get<Case>(`${this.apiUrl}/${id}`);
  }

  create(caseData: CreateCase): Observable<Case> {
    return this.http.post<Case>(this.apiUrl, caseData);
  }

  update(id: number, caseData: UpdateCase): Observable<Case> {
    return this.http.put<Case>(`${this.apiUrl}/${id}`, caseData);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
