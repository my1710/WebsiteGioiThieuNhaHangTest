using GioiThieuNhaHang.Data;
using GioiThieuNhaHang.Models.GioiThieuNhaHang.Services;
using GioiThieuNhaHang.Services; // Đúng namespace
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký AppDbContext và cấu hình chuỗi kết nối SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký EmailService và cấu hình
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<EmailService>();

// Nếu dùng ASP.NET Core 6+
builder.Services.AddSession();


// Đăng ký dịch vụ MVC + Session
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // THÊM TRƯỚC builder.Build()

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();



var app = builder.Build();
app.UseSession();

// Cấu hình pipeline xử lý HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseDeveloperExceptionPage(); // giúp hiện lỗi rõ hơn
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // giúp hiện lỗi rõ hơn
}



//app.UseHttpsRedirection();
app.UseStaticFiles(); // Phục vụ ảnh, CSS, JS...

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.UseSession(); //  Session Middleware

app.UseAuthorization();



// Định tuyến MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
