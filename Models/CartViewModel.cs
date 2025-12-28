using System.Collections.Generic;
using System.Linq;

namespace AlhalabiShopping.Models
{
    public class CartViewModel
    {
        // قائمة العناصر الموجودة في السلة
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        // حساب المجموع الكلي قبل الخصم
        public decimal TotalPrice => Items.Sum(item => item.Product.Price * item.Quantity);

        // حساب السعر النهائي بعد خصم الـ AI (مثلاً خصم 10%)
        public decimal DiscountedPrice => TotalPrice * 0.9m;
    }
}