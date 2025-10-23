using IKEA.BLL.Dto_s.DepartmentsDto_s;
using IKEA.BLL.Dto_s.EmployeeDto_s;
using IKEA.BLL.Services.Department;
using IKEA.BLL.Services.Employee;
using IKEA.DAL.Models.Employee;
using IKEA.DAL.Reposatories.EmployeeReposatory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using session03_MVC_.PL.ViewModels.DepartmentsVms;
using System.Buffers;


namespace session03_MVC_.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService service;
        private readonly ILogger logger;
        private readonly IWebHostEnvironment webHost;
        //private readonly IDepartmentService deptService;

        public EmployeeController(IEmployeeService service, ILogger<EmployeeController> logger, IWebHostEnvironment webHost, IDepartmentService DeptService)
        {
            this.service = service;
            this.logger = logger;
            this.webHost = webHost;
            //deptService = DeptService;
        }
        public IActionResult Index(string? searchValue)
        {
            if(searchValue is null)
            {

                var Employees = service.GetAllEmployees();
                return View(Employees);
            }
            else
            {
              
                return View(service.GetEmployees(searchValue));
            }


        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = deptService.GetAllDepartments(); //3shan ageeb el departments w a3redhom 
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]//action filter
        public IActionResult Create(CreateEmployeeDto dto)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    int res = service.AddEmployee(dto);
                    if (res > 0)
                    {
                        return RedirectToAction("Index"); //if its valid return to the same page 

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "dept cant be created");
                        return View(dto);
                    }

                }
                else
                    return View(dto); //give the same data in the inputs and gives the error msg 
            }
            catch (Exception ex)
            {
                //log errors 
                //development => in console 
                //production => in files or db

                if (webHost.IsDevelopment())
                {
                    logger.LogError(ex, "Error in Create Employee");

                }
                else
                {
                    //production 
                }
                return View(dto);
            }

        }

        [HttpGet]
        public IActionResult Details([FromRoute] int? id)
        {
            var dept = service.GetEmployeeById(id.Value);
            if (dept == null)
                return NotFound();
            return View(dept);
        }

        [HttpGet]
        public IActionResult Update([FromRoute] int? id)
        {
            var emp = service.GetEmployeeById(id.Value);
            if (emp == null)
                return NotFound();
            //why we need to create another View model for the view ??  
            //because the view need only some of the data in the dto or need to show it in another way
            //or the view need more data than the dto

            //we can use automapper to map between the dto and the view model
            //but here we will do it manually
            //we can use linq to map between the dto and the view model
            var viewEmployee = new UpdateEmployeeDto()
            {
                Id = emp.Id,
                Name = emp.Name,
                Address = emp.Address,
                Age = emp.Age,
                Email = emp.Email,
                HiringDate = emp.HiringDate,
                Salary = (int)emp.Salary,
                Gender = emp.Gender,
                EmployeeType = emp.EmployeeType,
                IsActive = emp.IsActive,
                PhoneNumber = emp.PhoneNumber,
               
            };
            return View(viewEmployee);
        }

        //POST: Handle form submission
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int? id, UpdateEmployeeDto emp)
        {
            if (id == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(emp);

            // Map VM => DTO (because the service layer expects DTO)
            var dto = new UpdateEmployeeDto()
            {
                Id = id.Value,
                Name = emp.Name,
                Address = emp.Address,
                Age = emp.Age,
                Email = emp.Email,
                HiringDate = emp.HiringDate,
                Salary = emp.Salary,
                Gender = emp.Gender,
                EmployeeType = emp.EmployeeType,
                IsActive = emp.IsActive,
                PhoneNumber = emp.PhoneNumber,
            };

            //_departmentService.UpdateDepartment(dto);


            try
            {
                if (ModelState.IsValid)
                {
                    int res = service.UpdateEmployee(dto);
                    if (res > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee could not be updated");
                        return View(emp);
                    }

                }
                else
                    return View(emp); //give the same data in the inputs and gives the error msg 
            }
            catch (Exception ex)
            {
                //log errors 
                //development => in console 
                //production => in files or db

                if (webHost.IsDevelopment())
                {
                    logger.LogError(ex, "Error in Create Department");

                }
                else
                {
                    //production 
                }
                return View(emp);
            }

        }


        ////[HttpGet]
        ////public IActionResult Delete([FromRoute] int? id)
        ////{
        ////    if (id is null)
        ////        return BadRequest();
        ////    var employee=service.GetEmployeeById(id.Value);
        ////    if(employee is null )
        ////        return NotFound();
        ////    return View(employee);
        ////}

        //[HttpPost]
        ////fucking hard delete 
        //public IActionResult Delete([FromRoute] int id)
        //{
        //    var message = string.Empty;
        //    try
        //    {
        //        var isDeleted = service.DeleteEmployee(id);

        //        if (isDeleted > 0)
        //        {
        //            return RedirectToAction("Index");
        //        }

        //        ModelState.AddModelError("", "Employee is not deleted.");
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {

        //        //log errors 
        //        //development => in console 
        //        //production => in files or db

        //        if (webHost.IsDevelopment())
        //        {
        //            logger.LogError(ex, "Error in Delete Employee");

        //        }
        //        else
        //        {
        //            //production 
        //        }

        //        return RedirectToAction("Index");
        //    }
        //}

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var employee = service.GetEmployeeById(id.Value);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var isDeleted = service.DeleteEmployee(id);

                if (isDeleted > 0)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Employee is not deleted.");
                return View();
            }
            catch (Exception ex)
            {
                if (webHost.IsDevelopment())
                {
                    logger.LogError(ex, "Error in Delete Employee");
                }
                else
                {
                    //production logging
                }
                return RedirectToAction("Index");
            }
        }


    }
}
