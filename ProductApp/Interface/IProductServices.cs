using ProductApp.Model;
using ProductApp.DTOs;

namespace ProductApp.Interface
{
    public interface IProductServices
    {
        List<ProductResponseDto> GetProducts();
        ProductResponseDto? GetproductById(int id);
        Product AddItem(Product product);
        Product? UpdateItem(Product product);
        Product? DeleteItem(int id);
    }
}
