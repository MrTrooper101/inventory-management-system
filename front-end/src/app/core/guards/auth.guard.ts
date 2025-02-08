import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) { }

  canActivate(): boolean {
    const isLoggedIn = !!localStorage.getItem('authToken'); // Check if token exists in local storage

    if (!isLoggedIn) {
      // Redirect to login page if user is not logged in
      this.router.navigate(['/login']);
      return false;
    }

    return true; // Allow access if logged in
  }
}
