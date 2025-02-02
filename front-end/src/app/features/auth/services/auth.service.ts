import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    private apiUrl = 'https://localhost:7101/api';

    constructor(private http: HttpClient) { }

    registerUser(userData: any): Observable<any> {
        return this.http.post<any>(`${this.apiUrl}/Authentication/register`, userData);
    }

    setPassword(data: { token: string; newPassword: string }): Observable<any> {
        return this.http.post<any>(`${this.apiUrl}/Authentication/password-setup`, data);
    }

    login(email: string, password: string): Observable<any> {
        const body = { email, password };
        return this.http.post<any>(`${this.apiUrl}/Authentication/login`, body);
    }

    saveToken(token: string): void {
        localStorage.setItem('authToken', token); // You can also use sessionStorage
    }

    getToken(): string | null {
        return localStorage.getItem('authToken');
    }

    logout(): void {
        localStorage.removeItem('authToken');
    }
}
