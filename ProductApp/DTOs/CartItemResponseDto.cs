namespace ProductApp.DTOs
{
    public class CartItemResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
