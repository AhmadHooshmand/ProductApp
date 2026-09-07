using ProductApp.DTOs;

namespace ProductApp.Interface
{
    public interface ICartServices
    {
        
        Task<CartResponseDto> GetCartByUserIdAsync(string userId);

       
        Task AddToCartAsync(string userId, int productId, int quantity);
    }
}
