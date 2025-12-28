using System;
using System.ComponentModel.DataAnnotations;

namespace AlhalabiShopping.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public int UserID { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        // أضفنا علامة الاستفهام لمنع تحذير الـ Null
        public string? DeliveryInfo { get; set; }

        // أضفنا هذه الخاصية لأن الـ OrderController يحتاجها
        public bool IsPaid { get; set; } = true; 
    }
}