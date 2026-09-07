
using global::ProductApp.Data;
using global::ProductApp.DTOs;
using global::ProductApp.Interface;
using global::ProductApp.Model;
using Microsoft.EntityFrameworkCore;
using ProductApp.Data;
using ProductApp.DTOs;
using ProductApp.Interface;
using ProductApp.Model;

namespace ProductApp.Services
{
    public class CartServices : ICartServices
    {
        private readonly AppDbContext _context;

        public CartServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddToCartAsync(string userId, int productId, int quantity)
        {
            // ۱. پیدا کردن سبد خرید کاربر یا ساخت سبد جدید اگر نداشت
            var cart = await _context.Carts
                .Include(c => c.Item)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            // ۲. بررسی اینکه آیا این محصول از قبل توی سبد بوده یا نه
            var cartItem = cart.Item.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem != null)
            {
                // اگه بود، فقط تعداد رو زیاد کن
                cartItem.Quantity += quantity;
            }
            else
            {
                // اگه نبود، آیتم جدید بساز و اضافه کن
                cart.Item.Add(new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<CartResponseDto> GetCartByUserIdAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Item)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return new CartResponseDto();
            }

            var response = new CartResponseDto
            {
                Items = cart.Item.Select(i => new CartItemResponseDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    Quantity = i.Quantity
                }).ToList(),
                TotalPrice =(decimal) cart.Item.Sum(i => i.Product.Price * i.Quantity)
            };

            return response;
        }
    }
}
