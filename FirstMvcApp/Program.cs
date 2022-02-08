using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Core.SerilogSinks;
using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;
using FirstMvcApp.DataAccess;
using FirstMvcApp.Domain.Services;
using FirstMvcApp.Filters;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FirstMvcApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((ctx, lc) =>
                {
                    lc.MinimumLevel.Information().WriteTo.Console();
                    lc.MinimumLevel.Debug().WriteTo.CustomSink();
                    lc.MinimumLevel.Fatal().WriteTo.File(@"C:\Users\AlexiMinor\Desktop\New folder (4)\log.log");
                });


            builder.Configuration
                .AddJsonFile("emailConfiguration.json")
                .AddInMemoryCollection();
                //.AddTxtConfiguration("123");

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

            //RegisterServices(builder.Services);
            // Add services to the container.
            builder.Services.AddScoped<IArticlesService, ArticlesService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
            builder.Services.AddScoped<IRepository<User>, UserRepository>();
            builder.Services.AddScoped<IRepository<Comment>, CommentsRepository>();
            builder.Services.AddScoped<ITestService, TestService>();
            builder.Services.AddScoped<IEmailSender, EmailService>();
          
            builder.Services.AddScoped<CustomFilter>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddControllersWithViews(opt =>
            {
                //opt.Filters.Add(typeof(SampleResourceFilter));
                //opt.Filters.Add(new SampleResourceFilter());
                opt.Filters.Add<SampleResourceFilter>();
            });

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

            app..Run();
        }

        private static IServiceCollection RegisterServices(IServiceCollection collection)
        {

            collection = collection.AddScoped<IEmailSender, EmailService>();
            return collection;
        }
    }
}

