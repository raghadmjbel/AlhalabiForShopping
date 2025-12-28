using Microsoft.EntityFrameworkCore;
using AlhalabiShopping.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// يبدأ التشغيل من صفحة Welcome
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Welcome}/{id?}");

app.Run();