import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  constructor(private _http: HttpClient) {}

  public uploadRequest(
    formData: FormData
  ): Observable<Product[]> {
    return this._http.post<Product[]>(
      'http://localhost:5273/api/product/recognize-and-search',
      formData
    );
  }
}
