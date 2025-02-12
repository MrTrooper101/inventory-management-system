import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CategoryService } from '../../../../services/category.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-category',
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, CommonModule],
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.scss'
})
export class AddCategoryComponent {
  categoryForm: FormGroup;
  private dialogRef = inject(MatDialogRef<AddCategoryComponent>);
  private categoryService = inject(CategoryService);
  private toastr = inject(ToastrService);

  constructor(
    private fb: FormBuilder,
  ) {
    this.categoryForm = this.fb.group({
      name: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      const categoryName = this.categoryForm.value.name;
      this.categoryService.addCategory(categoryName).subscribe(
        (response) => {
          this.toastr.success('Category added successfully');
          this.dialogRef.close(response); // Pass the response to the parent
        },
        (error) => {
          this.toastr.error('Error adding category');
          console.error('Error adding category', error);
        }
      );
    }
  }

  onCancel(): void {
    this.dialogRef.close(null); // Close dialog without action
  }
}
