import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Medicine } from '../models/medicine';

@Injectable({
  providedIn: 'root'
})
export class MedicineService {

  private readonly apiUrl =
    '/api/medicines';

  constructor(
    private http: HttpClient
  ) {}

  getMedicines(
    search?: string
  ): Observable<Medicine[]> {

    let url = this.apiUrl;

    if (search?.trim()) {

      url +=
        `?search=${encodeURIComponent(search.trim())}`;
    }

    return this.http.get<Medicine[]>(url);
  }

  addMedicine(
    medicine: Medicine
  ): Observable<Medicine> {

    return this.http.post<Medicine>(
      this.apiUrl,
      medicine
    );
  }
}