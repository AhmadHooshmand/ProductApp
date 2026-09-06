namespace ProductApp.Model
{
    public class CartItem
    {
        public int CartId { get; set; }
        public Cart Catr { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
