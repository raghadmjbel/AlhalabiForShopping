using System.ComponentModel.DataAnnotations;

namespace AlhalabiShopping.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; } = string.Empty;

        public string? ImagePath { get; set; }

        // العلاقة مع المنتجات (أضفنا علامة الاستقهام لمنع التحذير)
        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}