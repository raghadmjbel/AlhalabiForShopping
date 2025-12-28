using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlhalabiShopping.Data;

namespace AlhalabiShopping.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index() => View(await _context.Categories.ToListAsync());

        public async Task<IActionResult> CategoryProducts(int id)
        {
            var products = await _context.Products.Where(p => p.CategoryID == id).ToListAsync();
            return View(products);
        }
    }
}