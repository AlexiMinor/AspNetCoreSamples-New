using FirstMvcApp.Core.Interfaces;
using FirstMvcApp.Core.Interfaces.Data;
using FirstMvcApp.Data;
using FirstMvcApp.Data.Entities;
using FirstMvcApp.DataAccess;
using FirstMvcApp.Domain.Services;
using FirstMvcApp.Filters;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FirstMvcApp;

public class Startup
{
    public Startup()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("emailConfiguration.json")
            .AddInMemoryCollection();
        //.AddTxtConfiguration("123");

        Configuration = configurationBuilder.Build();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        var z = Configuration["isDebugMode"];
        var y = Configuration["data"];
        var x = Configuration["TEMP"];

        //builder.Configuration.AddInMemoryCollection(
        //    new Dictionary<string, string>()
        //    {
        //        { "fromEmail", "value" },
        //        { "emailPassword", "value2" }
        //    });

        //var inMemoryConfigData = builder.Configuration["key"];

        services.AddDbContext<NewsAggregatorContext>(opt
            => opt.UseSqlServer(connectionString));

        //RegisterServices(services);
        // Add services to the container.
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IRepository<User>, UserRepository>();
        services.AddScoped<IRepository<Comment>, CommentsRepository>();
        services.AddScoped<IRepository<Source>, SourceRepository>();
        services.AddScoped<IRepository<Role>, RoleRepository>();
        services.AddScoped<IRepository<UserRole>, UserRoleRepository>();
        services.AddScoped<IRepository<RefreshToken>, RefreshTokenRepository>();
        services.AddScoped<IArticlesService, ArticlesService>();
        services.AddScoped<ICommentService, CommentsService>();
        services.AddScoped<ITestService, TestService>();
        services.AddScoped<IEmailSender, EmailService>();
        services.AddScoped<ISourceService, SourceService>();
        services.AddScoped<IRssService, RssService>();
        services.AddScoped<IHtmlParserService, HtmlParserService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
       
        services.AddScoped<CustomFilter>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"),
                new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

        // Add the processing server as IHostedService
        services.AddHangfireServer();

        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.LoginPath = "/account/login";
                opt.AccessDeniedPath = "/access-denied";
            });

        services.AddAuthorization();

        services.AddControllersWithViews(opt =>
        {
            //opt.Filters.Add(typeof(SampleResourceFilter));
            //opt.Filters.Add(new SampleResourceFilter());
            opt.Filters.Add<SampleResourceFilter>();
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        app.UseHangfireDashboard();
        
        var rssService = serviceProvider.GetRequiredService<IRssService>();
            //RecurringJob.AddOrUpdate("Aggregation articles from rss",
            //    () => rssService.AggregateArticleDataFromRssSources(),
            //    "5 4 */13 * MON-FRI");
    }
}