using ProductApp.Model;

namespace ProductApp.Interface
{
    public interface ICategoryService
    {
        Category? GetById(int id);
        IEnumerable<Category> GetAll();
        Category Add(Category category);
        Category? Update(Category category);
        bool Delete(int id);
    }
}
