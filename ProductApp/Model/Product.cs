using System.ComponentModel.DataAnnotations;

namespace ProductApp.Model
{
    public class Product
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name can be at most 100 characters")]
        public string Name { get; set; } = "";

        [Range(0.01, 1000000000, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category{ get; set; }
    }
}
