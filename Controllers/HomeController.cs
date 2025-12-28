using Microsoft.AspNetCore.Mvc;

namespace AlhalabiShopping.Controllers
{
    public class HomeController : Controller
    {
        // UC01 – Open Main Interface
        public IActionResult Index()
        {
            // هذه الصفحة ستعرض "Alhalabi for Shopping"
            return View();
        }
    }
}
//minor UI enhancements 