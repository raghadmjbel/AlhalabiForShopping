using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlhalabiShopping.Data;
using AlhalabiShopping.Models;

namespace AlhalabiShopping.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // لوحة التحكم
        public async Task<IActionResult> Dashboard()
        {
            var model = new AdminDashboardViewModel
            {
                TotalProducts = await _context.Products.CountAsync(),
                TotalUsers = await _context.Users.CountAsync(),
                TotalOrders = await _context.Orders.CountAsync(),
                TotalRevenue = await _context.Orders.AnyAsync() ? await _context.Orders.SumAsync(o => o.TotalAmount) : 0,
                LatestOrders = await _context.Orders.OrderByDescending(o => o.OrderDate).Take(5).ToListAsync()
            };
            return View(model);
        }

        // عرض قائمة المنتجات
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // صفحة الإضافة - (تأكد أن اسم الملف Creatproduct.cshtml)
        [HttpGet]
        public IActionResult Creatproduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Creatproduct(Product product)
        {
            if (product != null)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // صفحة التعديل - فحص المنتج قبل إرساله للـ View
        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return RedirectToAction(nameof(Index)); // تجنب الـ NullReferenceException
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if (product != null)
            {
                _context.Attach(product);
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // حذف منتج
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // إدارة الطلبات
        public async Task<IActionResult> Manageorder()
        {
            var orders = await _context.Orders.OrderByDescending(o => o.OrderDate).ToListAsync();
            return View(orders);
        }
    }
}
// UI improvements applied