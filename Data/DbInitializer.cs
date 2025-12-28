using AlhalabiShopping.Models;
using System;
using System.Linq;

namespace AlhalabiShopping.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // إنشاء قاعدة البيانات إذا لم تكن موجودة
            context.Database.EnsureCreated();

            // 1. التحقق من وجود بيانات مسبقة (حتى لا تتكرر البيانات في كل تشغيل)
            if (context.Categories.Any())
            {
                return; // القاعدة ممتلئة مسبقاً
            }

            // 2. إضافة الأقسام الأربعة (UC04)
            var categories = new Category[]
            {
                new Category { CategoryName = "Make Up", ImagePath = "/images/cats/makeup.jpg" },
                new Category { CategoryName = "Furniture", ImagePath = "/images/cats/furniture.jpg" },
                new Category { CategoryName = "Food", ImagePath = "/images/cats/food.jpg" },
                new Category { CategoryName = "Bags", ImagePath = "/images/cats/bags.jpg" }
            };

            foreach (var c in categories) { context.Categories.Add(c); }
            context.SaveChanges();

            // 3. إضافة مستخدم Admin ومستخدم عادي (UC02, UC03)
            var users = new User[]
            {
                new User { FullName = "Ghaith & Raghad Admin", Email = "admin@halabi.com", Password = "Admin123", Role = "Admin" },
                new User { FullName = "Test User", Email = "user@halabi.com", Password = "User123", Role = "User" }
            };

            foreach (var u in users) { context.Users.Add(u); }
            context.SaveChanges();

            // 4. إضافة المنتجات (15 لكل قسم - سنضع مثالاً وتكملون الباقي بنفس النمط)
            var products = new Product[]
            {
                // Make Up (ID = 1)
                new Product { CategoryID = 1, ProductName = "Matte Lipstick", Price = 15.0m, AIScore = 0.95, ImagePath = "/images/makeup/1.jpg" },
                new Product { CategoryID = 1, ProductName = "Foundation", Price = 25.0m, AIScore = 0.88, ImagePath = "/images/makeup/2.jpg" },
                // ... أكمل حتى 15 عنصر لمكياج ...

                // Furniture (ID = 2)
                new Product { CategoryID = 2, ProductName = "Luxury Sofa", Price = 500.0m, AIScore = 0.92, ImagePath = "/images/furniture/1.jpg" },
                // ... أكمل حتى 15 عنصر أثاث ...
            };

            foreach (var p in products) { context.Products.Add(p); }
            context.SaveChanges();
        }
    }
}