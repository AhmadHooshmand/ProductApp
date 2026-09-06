namespace ProductApp.DTOs
{
    public class CartResponseDto
    {
        public List<CartItemResponseDto> Items { get; set; } = new List<CartItemResponseDto>();
        public Decimal TotalPrice { get; set; }
    }
}
