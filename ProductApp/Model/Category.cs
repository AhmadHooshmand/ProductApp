using System.ComponentModel.DataAnnotations;

namespace ProductApp.Model
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام دسته‌بندی اجباری است")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "نام دسته‌بندی باید بین ۳ تا ۵۰ کاراکتر باشد")]
        public string Name { get; set; } = "";
    }
}
