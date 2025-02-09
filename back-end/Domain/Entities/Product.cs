using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; } // Primary Key

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
        public bool IsActive { get; set; } = true;
    }
}
