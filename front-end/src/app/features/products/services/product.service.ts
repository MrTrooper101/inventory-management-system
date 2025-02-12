import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { Product } from '../models/product.model';
@Injectable({
    providedIn: 'root'
})
export class ProductService {
    readonly apiUrl = `${environment.apiUrl}/Product`;
    constructor(private http: HttpClient) { }

    addProduct(addProductData: Product): Observable<any> {
        return this.http.post(this.apiUrl, { addProductData });
    }

    getAllProductList(): Observable<any> {
        return this.http.get(this.apiUrl);
    }
}