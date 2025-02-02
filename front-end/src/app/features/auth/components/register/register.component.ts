import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AuthenticationService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  registrationForm: FormGroup;

  isLoading: boolean = false;

  registrationService = inject(AuthenticationService);
  router = inject(Router);
  toastr = inject(ToastrService);

  constructor(private fb: FormBuilder) {
    // Create the form group with validators
    this.registrationForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(50)]],
      middleName: ['', [Validators.maxLength(50)]],
      lastName: ['', [Validators.required, Validators.maxLength(50)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],  // Phone number validation for 10 digits
      companyName: ['', [Validators.required, Validators.maxLength(100)]],
      address: ['', [Validators.required, Validators.maxLength(200)]]
    });
  }

  ngOnInit(): void { }

  onSubmit(): void {
    if (this.registrationForm.valid) {
      this.isLoading = true;
      const userData = this.registrationForm.value;

      // Call the registration API
      this.registrationService.registerUser(userData).subscribe(
        response => {
          console.log(response);
          this.isLoading = false;
          // Show success toastr notification
          this.toastr.success('Registration successful! Redirecting...', 'Success');
          setTimeout(() => {
            this.router.navigate(['/login']);
          }, 2000);
        },  
        error =>(error: any) => {
          console.log(error);
          this.isLoading = false;
          // Show error toastr notification
          this.toastr.error('Registration failed. Please try again.', 'Error');
        }
      );
    } else {
      this.toastr.warning('Please fill in all required fields.', 'Warning');
    }
  }
}
