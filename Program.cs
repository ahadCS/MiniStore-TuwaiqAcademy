using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using mini_store.Data;
using mini_store;
using mini_store.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    });

 builder.Services.AddDbContext<AppDbContext>(Options=>Options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
 ));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// 2. تحديد اللغات المدعومة بالموقع 
var supportedCultures = new[] { "ar", "en-US" };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SetDefaultCulture(supportedCultures[0]);
    options.AddSupportedCultures(supportedCultures);
    options.AddSupportedUICultures(supportedCultures);

    // // إزالة مزود لغة المتصفح عشان يتحكم المستخدم باللغة  
    // var browserLanguageProvider = options.RequestCultureProviders
    //     .OfType<Microsoft.AspNetCore.Localization.AcceptLanguageHeaderRequestCultureProvider>()
    //     .FirstOrDefault();
    // if (browserLanguageProvider != null)
    // {
    //     options.RequestCultureProviders.Remove(browserLanguageProvider);
    // }
});

var app = builder.Build();


app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();