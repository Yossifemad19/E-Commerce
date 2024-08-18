using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Api.DTOs
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required, Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }

        [Required]
        public IFormFile Picture { get; set; }
        [Required]
        public int BrandID { get; set; }
        [Required]
        public int TypeID { get; set; }
    }
}
