// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2024 www.ebenmonney.com/mit-license
// ---------------------------------------

import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfigurationService } from './configuration.service';
import { Inmate } from '../models/inmate.model';

@Injectable({ providedIn: 'root' })
export class InmateService {

  private http = inject(HttpClient);
  private configurations = inject(ConfigurationService);

  /** Base API endpoint */
  private readonly baseUrl = `${this.configurations.baseUrl}/api/v1/inmates`;

  /** 
   * Retrieve all inmates 
   */
  getAll(): Observable<Inmate[]> {
    return this.http.get<Inmate[]>(this.baseUrl);
  }

  /**
   * Retrieve inmate by ID
   */
  getById(id: string): Observable<Inmate> {
    return this.http.get<Inmate>(`${this.baseUrl}/${id}`);
  }

  /**
   * Perform search based on filters
   */
  search(filters: any): Observable<Inmate[]> {
    let params = new HttpParams();
    Object.keys(filters).forEach(key => {
      if (filters[key]) {
        params = params.append(key, filters[key]);
      }
    });

    return this.http.get<Inmate[]>(`${this.baseUrl}/search`, { params });
  }

  /**
   * Create a new inmate record
   */
  create(inmate: Inmate): Observable<Inmate> {
    return this.http.post<Inmate>(this.baseUrl, inmate);
  }

  /**
   * Update an inmate record
   */
  update(id: string, inmate: Inmate): Observable<Inmate> {
    return this.http.put<Inmate>(`${this.baseUrl}/${id}`, inmate);
  }

  /**
   * Delete an inmate record
   */
  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
