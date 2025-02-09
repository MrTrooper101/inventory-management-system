import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { AddCategoryComponent } from './add-product/add-category/add-category.component';
import { firstValueFrom } from 'rxjs';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-product-category-list',
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatListModule, CommonModule, MatIconModule, MatButtonModule],
  templateUrl: './product-category-list.component.html',
  styleUrl: './product-category-list.component.scss'
})
export class ProductCategoryListComponent {
  searchForm: FormGroup;
  readonly dialog = inject(MatDialog);
  readonly dialogRef = inject(MatDialogRef<ProductCategoryListComponent>);
  readonly categoryService = inject(CategoryService);

  constructor(private fb: FormBuilder) {
    this.searchForm = this.fb.group({
      search: ['']
    });
  }

  categories: Array<any> = [];
  searchText: string = '';

  ngOnInit() {
    this.getAllCategories();
  }

  async getAllCategories() {
    const response = await firstValueFrom(this.categoryService.getAllCategoryList());
    if (response) {
      this.categories = response;
    }
  }

  // Filtered list based on search input
  filteredCategories() {
    if (!this.searchText.trim()) {
      return this.categories;
    }
    return this.categories.filter((category) =>
      category.name.toLowerCase().includes(this.searchText.toLowerCase())
    );
  }

  openAddProductCategoryDialog() {
    this.dialog.open(AddCategoryComponent, {})
  }

  selectCategory(category: any) {
    this.dialogRef.close(category); // Close the dialog and pass the selected category
  }
}
