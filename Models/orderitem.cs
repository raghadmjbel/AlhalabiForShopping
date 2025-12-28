namespace AlhalabiShopping.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = new Order();
        public int ProductId { get; set; } // تأكد أن هذا السطر موجود
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}