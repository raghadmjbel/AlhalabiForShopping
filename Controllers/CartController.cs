using Microsoft.AspNetCore.Mvc;
using AlhalabiShopping.Models;
using AlhalabiShopping.Data;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace AlhalabiShopping.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. عرض السلة مع حساب الخصم
        public IActionResult Index()
        {
            var cart = GetCart();
            decimal originalTotal = cart.Sum(i => i.Price);
            decimal finalTotal = originalTotal;

            if (originalTotal > 400)
            {
                finalTotal = originalTotal * 0.9m;
                ViewBag.DiscountMessage = "مبروك! حصلت على خصم 10% لتجاوزك مبلغ 400 $";
            }

            ViewBag.OriginalTotal = originalTotal;
            ViewBag.Total = finalTotal;
            return View(cart);
        }

        // 2. إضافة منتج للسلة
        [HttpPost]
        public async Task<IActionResult> Add(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                var cart = GetCart();
                cart.Add(product);
                SaveCart(cart);
            }
            // يعود لصفحة المنتجات (تأكد أن اسم الكنترولر Product)
            return RedirectToAction("Index", "Product");
        }

        // 3. حذف منتج واحد من السلة
        [HttpPost]
        public IActionResult Remove(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(p => p.ProductID == productId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        // 4. إتمام الطلب - تم التعديل لحل مشكلة SqlException
        [HttpPost]
        public async Task<IActionResult> Checkout(string address, string phone, string paymentMethod)
        {
            var cart = GetCart();
            if (cart == null || !cart.Any()) return RedirectToAction("Index");

            decimal originalTotal = cart.Sum(i => i.Price);
            decimal finalTotal = originalTotal > 400 ? originalTotal * 0.9m : originalTotal;

            // حل مشكلة نقص العمود في قاعدة البيانات:
            // ندمج طريقة الدفع داخل معلومات التوصيل لنتجنب خطأ Invalid column name 'PaymentMethod'
            string fullDeliveryInfo = $"العنوان: {address} | الهاتف: {phone} | طريقة الدفع: {paymentMethod}";

            var order = new Order
            {
                UserID = HttpContext.Session.GetInt32("UserId") ?? 1, // مستخدم افتراضي إذا لم يسجل دخول
                OrderDate = DateTime.Now,
                TotalAmount = finalTotal,
                DeliveryInfo = fullDeliveryInfo
                // ملاحظة: إذا قمت بعمل Migration لاحقاً، يمكنك إضافة حقل PaymentMethod هنا
            };

            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // تفريغ السلة بعد نجاح الحفظ
                HttpContext.Session.Remove("Cart");

                return RedirectToAction("Success", new { id = order.OrderID });
            }
            catch (Exception)
            {
                // في حال حدوث خطأ مفاجئ نعود للسلة
                return RedirectToAction("Index");
            }
        }

        // 5. صفحة النجاح
        public IActionResult Success(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        // دالة مساعدة لجلب السلة من الـ Session
        private List<Product> GetCart()
        {
            var data = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(data)) return new List<Product>();
            
            try 
            {
                return JsonSerializer.Deserialize<List<Product>>(data) ?? new List<Product>();
            }
            catch 
            {
                return new List<Product>();
            }
        }

        // دالة مساعدة لحفظ السلة في الـ Session
        private void SaveCart(List<Product> cart)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart, options));
        }
    }
}