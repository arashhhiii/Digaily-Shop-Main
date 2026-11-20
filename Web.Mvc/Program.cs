using Application.Class.Service.Implamentations;
using Application.Class.Service.Interfaces;
using Data.Class.Context;
using Data.Class.Repository;
using GoogleReCaptcha.V3;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// reCAPTCHA settings (supports both key spellings)
builder.Configuration["GoogleReCaptcha:SiteKey"] ??= builder.Configuration["googleReCaptcha.SiteKey"];
builder.Configuration["GoogleReCaptcha:SecretKey"] ??= builder.Configuration["googleReCaptcha.SecretKey"]
                                                        ?? builder.Configuration["googleReCaptcha.SecreteKey"];

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

// Services
builder.Services.AddHttpClient<ICaptchaValidator, GoogleReCaptchaValidator>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped(typeof(IgenricRepository<>), typeof(GenricRepository<>));
builder.Services.AddScoped<IProductService, ProductService>();

// Data Protection (registered once; keys are kept OUTSIDE wwwroot)
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "keys")))
    .SetApplicationName("Digaily")
    .SetDefaultKeyLifetime(TimeSpan.FromDays(7));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Authentication (cookie)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/register";
        option.LogoutPath = "/log-out";
        option.ExpireTimeSpan = TimeSpan.FromDays(7);
        option.SlidingExpiration = true;
    });   // <-- this closing was missing in the original

builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminEmail", policy => policy
        .RequireAuthenticatedUser()
        .RequireRole("Admin")
        .RequireAssertion(context => string.Equals(
            context.User.FindFirst("RoleId")?.Value,
            "1",
            StringComparison.Ordinal)));
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();