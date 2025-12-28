namespace AlhalabiShopping.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}