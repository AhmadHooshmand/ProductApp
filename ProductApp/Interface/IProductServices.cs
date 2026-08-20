using ProductApp.Model;

namespace ProductApp.Interface
{
    public interface IProductServices
    {
        List<Product> GetProducts();
        Product? GetproductById(int id);
        Product AddItem(Product product);
        Product? UpdateItem(Product product);
        Product? DeleteItem(int id);
    }
}
