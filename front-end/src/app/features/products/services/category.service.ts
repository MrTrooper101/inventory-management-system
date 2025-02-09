import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
@Injectable({
    providedIn: 'root'
})
export class CategoryService {
    readonly apiUrl = `${environment.apiUrl}/Category`;
    constructor(private http: HttpClient) { }

    addCategory(name: string): Observable<any> {
        return this.http.post(this.apiUrl, { name });
    }

    getAllCategoryList(): Observable<any> {
        return this.http.get(this.apiUrl);
    }
}