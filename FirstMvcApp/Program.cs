using FirstMvcApp.ConfigurationProviders;
using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;
using FirstMvcApp.DataAccess;
using FirstMvcApp.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace FirstMvcApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .AddJsonFile("emailConfiguration.json")
                .AddInMemoryCollection()
                .AddTxtConfiguration("123");

        

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            var z = builder.Configuration["isDebugMode"];
            var y = builder.Configuration["data"];
            var x = builder.Configuration["TEMP"];

            //builder.Configuration.AddInMemoryCollection(
            //    new Dictionary<string, string>()
            //    {
            //        { "fromEmail", "value" },
            //        { "emailPassword", "value2" }
            //    });
            
            //var inMemoryConfigData = builder.Configuration["key"];

            builder.Services.AddDbContext<NewsAggregatorContext>(opt
                => opt.UseSqlServer(connectionString));
            
            // Add services to the container.
            builder.Services.AddScoped<IArticlesService, ArticlesService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<ITestService, TestService>();
            builder.Services.AddScoped<IEmailSender, EmailService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddControllersWithViews();

            //builder.Services.AddScoped<INewsService, NewsService>();

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

        }
    }
}

