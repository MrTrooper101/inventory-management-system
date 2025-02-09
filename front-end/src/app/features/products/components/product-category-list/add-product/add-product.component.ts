import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { AddCategoryComponent } from './add-category/add-category.component';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';
import { ProductCategoryListComponent } from '../product-category-list.component';

@Component({
  selector: 'app-add-product',
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, CommonModule, MatSelectModule],
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.scss'
})
export class AddProductComponent {
  productForm!: FormGroup;
  categories: { id: number; name: string }[] = [
    { id: 1, name: 'Electronics' },
    { id: 2, name: 'Apparel' },
  ]; // Initial categories

  constructor(private fb: FormBuilder, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      price: [null, [Validators.required, Validators.min(1)]],
      description: [''],
      category: ['', Validators.required],
    });
  }

  openCategoryDialog(): void {
    const dialogRef = this.dialog.open(ProductCategoryListComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe((selectedCategory) => {
      if (selectedCategory) {
        this.productForm.patchValue({ category: selectedCategory.id });
      }
    });
  }
  onCancel() { }

  onSubmit(): void {
    if (this.productForm.valid) {
      console.log(this.productForm.value);
    }
  }
}
