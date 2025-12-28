using Microsoft.AspNetCore.Mvc;
using AlhalabiShopping.Data;
using AlhalabiShopping.Models;
using System;
using System.Linq;

namespace AlhalabiShopping.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // صفحة إدخال بيانات التوصيل - UC13
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        // تنفيذ عملية حفظ الطلب والدفع الوهمي - UC12, UC14
        [HttpPost]
        public IActionResult PlaceOrder(string address, string phone, decimal totalAmount)
        {
            // 1. التحقق من البيانات (FR14)
            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
            {
                ViewBag.Error = "يرجى إكمال بيانات التوصيل";
                return View("Checkout");
            }

            // 2. إنشاء كائن الطلب الجديد
            var newOrder = new Order
            {
                UserID = 1, // في المشروع الحقيقي نأخذه من Session.User
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                DeliveryInfo = $"العنوان: {address}, هاتف: {phone}",
                IsPaid = true // محاكاة الدفع الوهمي الناجح UC12
            };

            // 3. حفظ في قاعدة البيانات (FR14)
            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            // 4. توجيه المستخدم لصفحة النجاح
            return RedirectToAction("OrderSuccess", new { id = newOrder.OrderID });
        }

        public IActionResult OrderSuccess(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        // عرض تاريخ طلبات المستخدم
        public IActionResult MyOrders(int userId)
        {
            var orders = _context.Orders.Where(o => o.UserID == userId).ToList();
            return View(orders);
        }
    }
}