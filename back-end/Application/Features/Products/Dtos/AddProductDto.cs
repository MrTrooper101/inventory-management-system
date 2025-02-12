namespace back_end.Application.Features.Products.Dtos
{
    public class AddProductDto
    {

        public string? Name { get; set; } // Product Name

        public decimal Price { get; set; } // Product Price

        public int Quantity { get; set; } // Quantity in Stock

        public int CategoryId { get; set; } // Foreign Key to Category Table
    }
}
