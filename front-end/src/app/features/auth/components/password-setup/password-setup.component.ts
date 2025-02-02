import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from '../../services/auth.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-password-setup',
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, CommonModule],
  templateUrl: './password-setup.component.html',
  styleUrl: './password-setup.component.scss'
})
export class PasswordSetupComponent {
  passwordSetupForm: FormGroup;
  token: string | null = '';
  passwordMismatch = false;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private authService: AuthenticationService,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.passwordSetupForm = this.fb.group({
      newPassword: ['', [Validators.required, Validators.minLength(8)]],
      confirmNewPassword: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    // Get token from the URL
    this.token = this.route.snapshot.queryParamMap.get('token');
  }

  // Getter for form controls
  get newPassword() {
    return this.passwordSetupForm.get('newPassword');
  }

  get confirmNewPassword() {
    return this.passwordSetupForm.get('confirmNewPassword');
  }

  // Check if the passwords match
  onPasswordChange(): void {
    this.passwordMismatch = this.newPassword?.value !== this.confirmNewPassword?.value;
  }

  // Handle form submission
  onSubmit(): void {
    if (this.passwordSetupForm.valid && this.token) {
      const { newPassword, confirmNewPassword } = this.passwordSetupForm.value;

      // Check if the new password and confirm password match
      if (newPassword !== confirmNewPassword) {
        this.toastr.error('Passwords do not match!', 'Error');
        return;
      }

      debugger;
      this.authService.setPassword({ token: this.token, newPassword: confirmNewPassword })
        .subscribe(
          response => {
            this.toastr.success('Password has been successfully updated!', 'Success');
            this.router.navigate(['/login']); // Redirect to login page
          },
          error => {
            this.toastr.error('Failed to update password. Please try again.', 'Error');
          }
        );
    }
  }
}
