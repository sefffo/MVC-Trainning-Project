using IKEA.BLL.Common.MappingProfiles;
using IKEA.BLL.Services.Department;
using IKEA.BLL.Services.Employee;
using IKEA.DAL.Contexts;
using IKEA.DAL.Reposatories.DepartmentReposatory;
using IKEA.DAL.Reposatories.EmployeeReposatory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace session03_MVC_.PL
{
    public class Program
    {
        /*
         * 
                | Feature       | `IEnumerable`                                                         | `IQueryable`                                                                    |
                | ------------- | --------------------------------------------------------------------- | ------------------------------------------------------------------------------- |
                | **Namespace** | `System.Collections` / `System.Collections.Generic`                   | `System.Linq`                                                                   |
                | **Purpose**   | Used to iterate over in-memory collections (like lists, arrays, etc.) | Used to build and execute queries against remote data sources (like a database) |
    


                                                                                    
                                               hygeb el data koha w msh daymn ana m7tag da w momken y3ml delay in system            by3ml excute fe el db al awl w yrg3 el matlop

                | Feature                     | `IEnumerable`                                                                      | `IQueryable`                                                                                                                      |
                | --------------------------- | ---------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------- |
                | **Execution**               | **Deferred execution in memory** – fetches all data first, then filters in-memory. | **Deferred execution at the data source** – builds an **expression tree** that is translated into SQL and executed on the server. |
                | **Where filtering happens** | On the **client (in-memory)**                                                      | On the **server (database)**                                                                                                      |



                | Feature           | `IEnumerable`                                                                              | `IQueryable`                                                               |
                | ----------------- | ------------------------------------------------------------------------------------------ | -------------------------------------------------------------------------- |
                | **Performance**   | Less efficient for large data sets because it loads all data into memory before filtering. | More efficient — only retrieves the required data from the database.       |
                | **Best Use Case** | Working with **small or already-loaded collections**.                                      | Working with **Entity Framework, LINQ to SQL**, or any remote data source. |




                | Feature         | `IEnumerable`                                                | `IQueryable`                                                                                |
                | --------------- | ------------------------------------------------------------ | ------------------------------------------------------------------------------------------- |
                | **Translation** | Executes LINQ-to-Objects.                                    | Converts LINQ expressions into SQL (or another query language).                             |
                | **Example**     | `.Where(x => x.Age > 30)` filters **after data is fetched**. | `.Where(x => x.Age > 30)` gets **translated into SQL WHERE Age > 30** before fetching data. |



                List<Employee> employees = db.Employees.ToList(); // Data is already loaded
                var result = employees.Where(e => e.Age > 30);   // Filtering done in memory

                IQueryable<Employee> employees = db.Employees;    // Query not executed yet
                var result = employees.Where(e => e.Age > 30);    // Translates to SQL WHERE Age > 30
                var list = result.ToList();                       // Query executes here








                | Feature                         | **TempData**                                                                                        | **ViewData**                                        | **ViewBag**                                                        |
                | ------------------------------- | --------------------------------------------------------------------------------------------------- | --------------------------------------------------- | ------------------------------------------------------------------ |
                | **Namespace**                   | `Microsoft.AspNetCore.Mvc.ViewFeatures`                                                             | `Microsoft.AspNetCore.Mvc.ViewFeatures`             | Dynamic wrapper around `ViewData`                                  |
                | **Type**                        | `TempDataDictionary`                                                                                | `ViewDataDictionary`                                | Dynamic (uses `ViewData` internally)                               |
                | **Lifetime**                    | **Persists between two requests** (Redirects)                                                       | **Only for the current request**                    | **Only for the current request**                                   |
                | **Usage**                       | To pass data between actions (e.g., Redirects)                                                      | To pass data from Controller => View                 | To pass data from Controller => View                                |
                | **Type Safety**                 | Not type-safe (needs casting)                                                                       | Not type-safe (needs casting)                       | Type-safe (dynamic)                                                |
                | **Underlying Storage**          | Session or cookie (depends on configuration)                                                        | Dictionary stored in memory during request          | Uses `ViewData` internally                                         |
                | **Syntax Example (Controller)** | `TempData["msg"] = "Saved!";`                                                                       | `ViewData["msg"] = "Saved!";`                       | `ViewBag.msg = "Saved!";`                                          |
                | **Syntax Example (View)**       | `@TempData["msg"]`                                                                                  | `@ViewData["msg"]`                                  | `@ViewBag.msg`                                                     |
                | **Best Use Case**               | When redirecting and you need to pass a message (e.g., success message after POST => Redirect => GET) | When passing data to a view that only needs it once | Same as ViewData, but easier to read/write with dynamic properties |


          
         */













        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(
                Options => Options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute)
                );
            builder.Services.AddDbContext<APPDbContext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));//3shan ygeb el connection string mn el appsettings.json
                                                                                                     //it will be more flexiable too for test and production 
            });

            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();//inject the repo to be used in the service layer
            //ay 7d ytlob IDepartmentRepo 5leh ygeb DepartmentRepo


            builder.Services.AddScoped<IDepartmentService, DepartmentServices>();//inject the service to be used in the controller layer


            builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();//inject the repo to be used in the service layer
            //ay 7d ytlob IDepartmentRepo 5leh ygeb EmployeeRepo
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddAutoMapper(m => m.AddMaps(typeof(ProjectMapperProfile).Assembly));

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
