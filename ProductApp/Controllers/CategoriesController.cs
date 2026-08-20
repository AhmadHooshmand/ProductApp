using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Interface;
using ProductApp.Model;
using ProductApp.Services;

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound("دسته بندی یافت نشد.");
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            var newCategory = _categoryService.Add(category);
            return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
        }

        [HttpPut]
        public IActionResult Update(Category category)
        {
            var updatedCategory = _categoryService.Update(category);
            if (updatedCategory == null)
            {
                return NotFound("دسته بندی برای آپدیت پیدا نشد.");
            }
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedCategory = _categoryService.Delete(id);
            if (deletedCategory == false)
            {
                return NotFound("دسته بندی برای حذف پیدا نشد.");
            }
            return Ok(new { Message = "دسته بندی حذف شد", Category = deletedCategory });
        }
    }
}

