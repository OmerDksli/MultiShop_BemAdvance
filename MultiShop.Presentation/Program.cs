using MediatR;
using Microsoft.EntityFrameworkCore;
using MultiShop.Business.MediatR.Handlers;
using MultiShop.Business.MediatR.Queries;
using MultiShop.Data;
using MultiShop.Presentation.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);//web uygulamas� olu�turulur 

builder.Services.AddControllersWithViews(); //1) Controller views alt yap�s�n� entegre ediyoruz


//Servis Entegrations 
builder.Services.AddCustomServices();

//4:for connection db 
//bunun set edildi�i yer MultiShopDbContext i�erisindeki constructor metottur.
builder.Services.AddDbContext<MultiShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MultiShopConnectionStr_Prod"));
});

//Step2 : AutoMapper
builder.Services.AddCustomAutoMapper();

//Sieve Configuration 
builder.Services.AddCustomSieveServices(builder.Configuration);

//Add mediatR
builder.Services.AddMediatR(m => m.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<IRequestHandler<GetCategoriesQuery, List<CategoryDto>>, GetCategoriesQueryHandler>();



builder.Services.AddSession();//Session kullanmak i�in eklenmelidir

var app = builder.Build();

app.UseRouting(); //2

app.MapControllerRoute(name: "default", pattern: "{Controller=Home}/{action=Index}/{id?}");//2

app.UseStaticFiles();//3

//UserController/Index -> User/Index/id
//? optional

app.UseSession();

app.Run();
