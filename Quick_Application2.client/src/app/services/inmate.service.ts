import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Inmate } from '../models/inmate.model';

@Injectable({ providedIn: 'root' })
export class InmateService {
  private baseUrl = '/api/v1/inmates';

  constructor(private http: HttpClient) { }

  search(filters: any): Observable<Inmate[]> {
    let params = new HttpParams();

    if (filters.inmateId)
      params = params.set('inmateId', filters.inmateId);

    if (filters.firstName)
      params = params.set('firstName', filters.firstName);

    if (filters.lastName)
      params = params.set('lastName', filters.lastName);

    if (filters.facility)
      params = params.set('facilityId', filters.facility);

    if (filters.status)
      params = params.set('status', filters.status);

    if (filters.bookingDate)
      params = params.set('bookingDateFrom', filters.bookingDate);

    return this.http.get<Inmate[]>(`${this.baseUrl}/search`, { params });
  }


}
