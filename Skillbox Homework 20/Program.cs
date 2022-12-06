using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Skillbox_Homework_20.Database;
using System.Numerics;

namespace Skillbox_Homework_20
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<myContext>();
                    SampleData.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }
            public static IHostBuilder CreateHostBuilder(string[] args)
                => Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(
                        webBuilder => webBuilder.UseStartup<Startup>());
        }

    public class Startup
    {
        public IConfiguration Configuration{ get;}
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("myPathSQL");
            services.AddDbContext<myContext>(options => options.UseSqlServer(connection));
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }

    public static class SampleData
    {
        public static void Initialize(myContext context)
        {
            if (!context.Information.Any())
            {
                context.Information.AddRange(
                    new Information
                    {
                        SecondName = "Stevenson",
                        FirstName = "Matthew",
                        Patronymic = "Steve",
                        Description = "I'm a person",
                        PhoneNumber = "123132",
                        Adress = "New York city"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}