using ProductApp.Data;
using ProductApp.Interface;
using ProductApp.Model;

namespace ProductApp.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }


        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
            
        }

        public bool Delete(int id)
        {
            Category? existingCategory = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (existingCategory is null)
            {
                return false;
            }

            _context.Categories.Remove(existingCategory);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category? GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public Category Update(Category category)
        {
            Category? existingCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);

            if (existingCategory is null)
            {
                return category;
            }

            existingCategory.Name = category.Name;
            _context.SaveChanges();
            return existingCategory;
        }
    }
}
