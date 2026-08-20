import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Sale } from '../models/sale';
import { SaleRequest } from '../models/salerequest';

@Injectable({
  providedIn: 'root'
})
export class SaleService {

  private readonly apiUrl =
    '/api/sales';

  constructor(
    private http: HttpClient
  ) {}

  getSales(): Observable<Sale[]> {
    return this.http.get<Sale[]>(this.apiUrl);
  }

  sellMedicine(
    request: SaleRequest
  ): Observable<Sale> {

    return this.http.post<Sale>(
      this.apiUrl,
      request
    );
  }
}