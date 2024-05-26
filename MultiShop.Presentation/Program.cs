using Microsoft.EntityFrameworkCore;
using MultiShop.Business.Interfaces;
using MultiShop.Data;
using MultiShop.Repository;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);//web uygulamas� olu�turulur 



builder.Services.AddControllersWithViews(); //1) Controller views alt yap�s�n� entegre ediyoruz


//Servis Entegrasyonu
builder.Services.AddScoped<ICategoryService, CategoryService>();

//Repository ntegrasyonu 
builder.Services.AddScoped<ICategoryRepository, ProductsRepository>();

builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<ISliderRepository, SliderRepository>();

#region product
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
#endregion

#region productRating
builder.Services.AddScoped<IProductRatingService, ProductRatingService>();
builder.Services.AddScoped<IProductRatingRepository, ProductRatingRepository>();
#endregion

#region productImage
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
#endregion


//4:for connection db 
//bunun set edildi�i yer MultiShopDbContext i�erisindeki constructor metottur.
builder.Services.AddDbContext<MultiShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MultiShopConnectionStr_Prod"));
});
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MultiShopDbContext>();

//Step2 : AutoMapper
builder.Services.AddAutoMapper(typeof(CategoryMappingProfile));
builder.Services.AddAutoMapper(typeof(SliderMappingProfile));


var app = builder.Build();


app.UseRouting(); //2

app.MapControllerRoute(name: "default", pattern: "{Controller=Home}/{action=index}/{id?}");//2

app.UseStaticFiles();//3

//UserController/Index -> User/Index/id
//? optional

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MultiShopDbContext>();
    //var deneme=context.Products.ToList();
   if(!context.Categories.Any())
    {
        // IWebHostEnvironment servisini al
        var env = services.GetRequiredService<IWebHostEnvironment>();



        string pathproducts = Path.Combine(env.WebRootPath, @"DumyData\products.json");//dosya yolunu al�r
        string products = File.ReadAllText(pathproducts);//dosyay� verisini string de�i�kene atar

        string pathcategories = Path.Combine(env.WebRootPath, @"DumyData\categories.json");
        string categories = File.ReadAllText(pathcategories);

        string pathproductRatings = Path.Combine(env.WebRootPath, @"DumyData\productRatings.json");
        string productRatings = File.ReadAllText(pathproductRatings);

        string pathproductImages = Path.Combine(env.WebRootPath, @"DumyData\productImages.json");
        string productImages = File.ReadAllText(pathproductImages);

        string pathSliders = Path.Combine(env.WebRootPath, @"DumyData\sliders.json");
        string sliders = File.ReadAllText(pathSliders);


        //json verilerini objelere d�n��t�r�r
        var productObject = JsonConvert.DeserializeObject<List<Product>>(products);
        var categoriesObject = JsonConvert.DeserializeObject<List<Category>>(categories);
        
        var productRatingsObject = JsonConvert.DeserializeObject<List<ProductRating>>(productRatings);
        var productImagesObject = JsonConvert.DeserializeObject<List<ProductImage>>(productImages);
        var slidersObject = JsonConvert.DeserializeObject<List<Slider>>(sliders);
        if (productObject != null && categoriesObject != null && productRatingsObject != null && productImagesObject != null && slidersObject != null)
        {
            //foreach (var item in categoriesObject)
            //{
            //    item.CategoryId = null;
            //}
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Categories ON");
            context.AddRange(categoriesObject);

            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Categories OFF");
        }
    }


   


}



app.Run();
