import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AuthenticationService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, FormsModule, CommonModule, MatFormFieldModule, MatInputModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm: FormGroup;

  authService = inject(AuthenticationService);
  router = inject(Router);

  constructor(private fb: FormBuilder) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],  // Email field
      password: ['', [Validators.required, Validators.minLength(6)]]  // Password field
    });
  }

  ngOnInit(): void { }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;

      // Call login API using the AuthService
      this.authService.login(email, password).subscribe(
        (response) => {
          if (response.token) {
            this.authService.saveToken(response.token);  // Save token to local storage
            // this.router.navigate(['/dashboard']);  // Navigate to the dashboard or other route
          } else {
            console.log('Login failed');
          }
        },
        (error) => {
          console.error('Login failed', error);
        }
      );
    } else {
      console.log('Form is invalid');
    }
  }
}
