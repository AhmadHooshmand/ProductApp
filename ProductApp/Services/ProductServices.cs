using Microsoft.AspNetCore.Http.HttpResults;
using ProductApp.Data;
using ProductApp.Interface;
using ProductApp.Model;

namespace ProductApp.Services
{
    public class ProductServices:IProductServices
    {
        private readonly AppDbContext _context;

        public ProductServices(AppDbContext context)
        {
            _context = context;
        }

        public Product AddItem(Product product)
        {
            
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product? DeleteItem(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return null;
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return product;
        }

        public Product? GetproductById(int id)
        {
            return _context.Products.Find(id);
        }

        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public Product? UpdateItem(Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            _context.SaveChanges();
            return existingProduct;
        }
    }
}
