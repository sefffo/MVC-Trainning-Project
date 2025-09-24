using IKEA.DAL.Contexts;
using IKEA.DAL.Reposatories.DepartmentReposatory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace session03_MVC_.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<APPDbContext>(options=> {
            
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));//3shan ygeb el connection string mn el appsettings.json
                                                                                //it will be more flexiable too for test and production 
            });
           
            builder.Services.AddScoped<IDepartmentRepo,DepartmentRepo>();//inject the repo to be used in the service layer
            //ay 7d ytlob IDepartmentRepo 5leh ygeb DepartmentRepo
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseHttpsRedirection();
            app.UseRouting();


            app.UseStaticFiles();//files
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
               

            app.Run();
        }
    }
}
