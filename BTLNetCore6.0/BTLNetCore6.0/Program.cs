using AspNetCoreHero.ToastNotification;
using BTLNetCore6._0.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


// Gọi lại chuỗi kết nối đến database
var stringConnectdb = builder.Configuration.GetConnectionString("BTL");
builder.Services.AddDbContext<webtintucContext>(options => options.UseSqlServer(stringConnectdb)); // "btlNetCoreContext" là lớp "btlNetCoreContext.cs" trong phần model

// Đọa code này nghĩa là: Cho trang web hiểu những từ tiếng Việt có dấu và chèn thêm tiếng Việt vào trong trang web
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    // Chỉ ra đường dẫn cấm truy cập cho dù đã đăng nhập
    //options.AccessDeniedPath = new PathString("/Manager/Account");
    //options.Cookie = new CookieBuilder
    //{
    //    HttpOnly = true,
    //    Name = "abc", // Đặt tên
    //    Path= "/", // Đây là chỉ ra đường dẫn mặc định
    //    SameSite = SameSiteMode.Lax,
    //    SecurePolicy = CookieSecurePolicy.SameAsRequest
    //};
    // Chỉ ra đường dẫn login
    options.LoginPath = new PathString("/Home/Index");
    options.AccessDeniedPath = "/Error/AccessDenied"; // cấu hình đường dẫn tới trang AccessDenied
    // Câu lệnh này là khi người dùng cố tình nhảy vào 1 tài nguyên của Admin thì sẽ hệ thống sẽ tự nhảy đến đường dẫn là "/Home/Login"
    // Tham số "redirect" này khi người dùng đăng nhập thành công thì sẽ đưa đến đường dẫn này
    options.ReturnUrlParameter = "url";
    // Thiết lập thời gian sống của Cookie
    options.SlidingExpiration = true;

    // Đây là phân quyền trong file "program"
    // Phân quyền người dung
    //builder.Services.AddAuthorization(options =>
    //{
    //    // Chỉ định người dùng có quyền là "Admin" thì mới vào được controller là "RequireAdminRole"
    //    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    //});
});

// Đây là phần phân trang
//builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
builder.Services.AddDistributedMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddNotyf(config => {
    config.DurationInSeconds = 10;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseStaticFiles();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
