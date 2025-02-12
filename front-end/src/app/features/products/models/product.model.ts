export interface Product {
    id?: number;           // Primary Key
    name: string;         // Product Name
    price: number;        // Product Price
    quantity: number;     // Quantity in Stock
    categoryId: number;   // Foreign Key to Category
}
