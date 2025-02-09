import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { AddProductComponent } from './product-category-list/add-product/add-product.component';

@Component({
  selector: 'app-products',
  imports: [FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent {
  products = [
    { name: 'Product 1', price: 100, description: 'Description for Product 1' },
    { name: 'Product 2', price: 200, description: 'Description for Product 2' },
  ];

  constructor(private dialog: MatDialog) { }

  openAddProductDialog(): void {
    const dialogRef = this.dialog.open(AddProductComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe((newProduct) => {
      if (newProduct) {
        this.products.push(newProduct);
      }
    });
  }
}
