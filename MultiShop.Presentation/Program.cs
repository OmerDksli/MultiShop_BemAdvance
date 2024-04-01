using Microsoft.EntityFrameworkCore;
using MultiShop.Business;
using MultiShop.Data;
using MultiShop.Repository;

var builder = WebApplication.CreateBuilder(args);//web uygulamas� olu�turulur 


builder.Services.AddControllersWithViews(); //1) Controller views alt yap�s�n� entegre ediyoruz

//Servis Entegrasyonu
builder.Services.AddScoped<ICategoryService, CategoryService>();

//Repository ntegrasyonu 
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


//4:for connection db 
//bunun set edildi�i yer MultiShopDbContext i�erisindeki constructor metottur.
builder.Services.AddDbContext<MultiShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MultiShopConnectionStr_Prod"));
});

var app = builder.Build();


app.UseRouting(); //2

app.MapControllerRoute(name: "default", pattern: "{Controller=Home}/{action=Index}/{id?}");//2

app.UseStaticFiles();//3

//UserController/Index -> User/Index/id
//? optional




app.Run();
