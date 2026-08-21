using Microsoft.AspNetCore.Http.HttpResults;
using ProductApp.Data;
using ProductApp.DTOs;
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

        public ProductResponseDto? GetproductById(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return null;
            }

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId
            };
        }


        public List<ProductResponseDto> GetProducts()
        {
            return _context.Products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId
            }).ToList();
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
