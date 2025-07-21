using BLL.Services.Abstract;
using BLL.Services.Concretes;
using DAL.Context;
using DAL.Repository.Abstract;
using DAL.Repository.Concretes;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IServiceManager<>), typeof(ServiceManager<>));
builder.Services.AddScoped<IEnglishWordService, EnglishWordService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IEnglishWordRepository, EnglishWordRepository>();
builder.Services.AddScoped<IQuizService, QuizService>();





builder.Services.AddDbContext<WordContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("EgeConnection")));

builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi (boþta 30 dakika)
    options.Cookie.HttpOnly = true;                 // JavaScript eriþemesin
    options.Cookie.IsEssential = true;              // GDPR uyumu için gerekli
}); // Session sistemini ekle

var accessKeyFromConfig = builder.Configuration["AccessControl:AccessKey"];


var app = builder.Build();


app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Use(async (context, next) =>
{
    var userKey = context.Request.Query["accessKey"].ToString();

    if (userKey != accessKeyFromConfig)
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("Yetkisiz eriþim.");
        return;
    }

    await next(); // Doðruysa devam et
});

app.Run();
