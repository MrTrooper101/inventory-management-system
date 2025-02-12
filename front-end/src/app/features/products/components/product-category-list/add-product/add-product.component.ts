import { Component, inject } from '@angular/core';
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
import { Product } from '../../../models/product.model';
import { ProductService } from '../../../services/product.service';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from '../../../services/category.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-add-product',
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatIconModule, CommonModule, MatSelectModule],
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.scss'
})
export class AddProductComponent {
  productForm!: FormGroup;
  categories: Array<any> = [];

  private dialogRef = inject(MatDialogRef<AddProductComponent>);
  private dialog = inject(MatDialog);
  private productService = inject(ProductService);
  private categoryService = inject(CategoryService);
  private toastNotificationService = inject(ToastrService);

  constructor(private fb: FormBuilder) {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      price: [null, [Validators.required, Validators.min(1)]],
      quantity: [null, [Validators.required, Validators.min(1)]],
      description: [''],
      category: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.getAllCategories();
  }

  async getAllCategories() {
    this.categoryService.getAllCategoryList().subscribe((response) => {
      if (response) {
        this.categories = response;
      }
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

  async onSubmit() {
    try {
      if (!this.productForm.valid) {
        return;
      }

      debugger;
      const addProductData: Product = {
        name: this.productForm.value.name,
        price: this.productForm.value.price,
        quantity: this.productForm.value.quantity,
        categoryId: this.productForm.value.category,
      }

      console.log(addProductData);
      const response = await firstValueFrom(this.productService.addProduct(addProductData));

      console.log(response); 
      if (response) {
        // this.toastNotificationService.success('Product added successfully');
        // this.dialogRef.close(true);
      }
    }
    catch (error: any) {
      console.error(error);
      this.toastNotificationService.error('Failed to add product');
    }
  }
}
