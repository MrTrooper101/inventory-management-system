using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace back_end.Application.Features.Products.Dtos
{
    public class ProductDto
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } // Product Name

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; } // Product Price

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; } // Quantity in Stock

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; } // Foreign Key to Category Table

        [Required]
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
