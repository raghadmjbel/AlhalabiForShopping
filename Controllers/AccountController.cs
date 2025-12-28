using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlhalabiShopping.Data;
using AlhalabiShopping.Models;
using Microsoft.AspNetCore.Http;

namespace AlhalabiShopping.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. صفحة الترحيب (الـ ENTER)
        [HttpGet]
        public IActionResult Welcome()
        {
            return View();
        }

        // 2. خيارات الدخول أو التسجيل
        [HttpGet]
        public IActionResult AuthOptions()
        {
            return View();
        }

        // 3. صفحة تسجيل الدخول (عرض)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // 4. معالجة تسجيل الدخول
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // حفظ البيانات في الجلسة (Session)
                HttpContext.Session.SetInt32("UserId", user.UserID);
                HttpContext.Session.SetString("UserName", user.FullName ?? "مستخدم");
                HttpContext.Session.SetString("UserRole", user.Role ?? "Customer");

                if (user.Role == "Admin")
                {
                    // يذهب للوحة التحكم (تأكد من وجود AdminController ودالة Dashboard)
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    // يذهب لصفحة الأقسام
                    return RedirectToAction("Index", "Product");
                }
            }

            ViewBag.Error = "الإيميل أو كلمة المرور غلط.. تأكد منهن يا بطل!";
            return View();
        }

        // 5. صفحة إنشاء حساب جديد (عرض)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // 6. معالجة إنشاء حساب جديد (مُعدلة لضمان النجاح)
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            try 
            {
                // نضع القيم الافتراضية لمنع أخطاء قاعدة البيانات (Nulls)
                user.Role = "Customer"; 
                
                // إضافة المستخدم لقاعدة البيانات
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                
                // النجاح: نرسله لصفحة تسجيل الدخول
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                // إذا صار خطأ، نعرض رسالة واضحة بدل "الكتابة الكثيرة"
                ViewBag.Error = "صار مشكلة بالتسجيل.. ممكن الإيميل مستخدم من قبل أو البيانات ناقصة.";
                return View(user);
            }
        }

        // 7. تسجيل الخروج
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Welcome");
        }
    }
}