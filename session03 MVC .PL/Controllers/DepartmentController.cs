using Humanizer;
using IKEA.BLL.Dto_s.DepartmentsDto_s;
using IKEA.BLL.Services.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using session03_MVC_.PL.ViewModels.DepartmentsVms;

namespace session03_MVC_.PL.Controllers
{
    public class DepartmentController : Controller
    {

        public readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment webHost;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment webHost)
        {
            _departmentService = departmentService;
            this.logger = logger;
            this.webHost = webHost;
        }
        //DepartmentServices _departmentServices = new DepartmentServices(); 
        // we cant do that because the service need the repo so we use dependency injection
        public IActionResult Index()
        {
            var depts = _departmentService.GetAllDepartments();
            return View(depts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto dto)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    int res = _departmentService.AddDepartment(dto);
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
                    logger.LogError(ex, "Error in Create Department");

                }
                else
                {
                    //production 
                }
                return View(dto);
            }

        }

        [HttpGet]
        public IActionResult Details([FromRoute]int? id)
        {
            var dept = _departmentService.GetDepartmentById(id.Value);
            if (dept == null)
                return NotFound();
            return View(dept);
        }

        //GET: Show form with existing data
        /*
         => Why we often prefer a ViewModel

        Even if it looks redundant, creating a DepartmentVm has important advantages:

        The view might not need all DTO properties

            DTO may have things like CreatedAt, UpdatedBy, IsDeleted, etc.

            If the view only edits Name, Code, Description, why pass extra fields?

        The view might need more than the DTO

            Example: a dropdown list of Managers (List<SelectListItem>).

            DTO doesn’t know about UI helpers — ViewModel can include them.

       we re in that case =>>> Different views, different needs

            UpdateDepartmentViewModel for editing might only have editable fields.

            DepartmentDetailsViewModel for details page might include read-only fields and related entities.

        Security (Overposting attacks)

            If you bind your view directly to a DTO that has extra properties, a malicious user could send extra fields in the form and overwrite data you didn’t intend to expose.

            ViewModel ensures only intended properties are bound.

        Separation of Concerns

            DTO = transfer between layers (BLL ↔ Controller).

            ViewModel = tailored for the view (Controller ↔ Razor Page).

            Keeps layers clean and decoupled.
         
         */
        [HttpGet]
        public IActionResult Update([FromRoute] int? id)
        {
            var dept = _departmentService.GetDepartmentById(id.Value);
            if (dept == null)
                return NotFound();
            //why we need to create another View model for the view ??  
            //because the view need only some of the data in the dto or need to show it in another way
            //or the view need more data than the dto

            //we can use automapper to map between the dto and the view model
            //but here we will do it manually
            //we can use linq to map between the dto and the view model
            var ViewDepartment = new DepartmentVm()
            {
                Id = dept.Id,
                Name = dept.Name,
                code = dept.code,
                description = dept.description
            };  
            return View(ViewDepartment);
        }

        //POST: Handle form submission
        [HttpPost]
        public IActionResult Update([FromRoute]int ? id ,DepartmentVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // Map VM → DTO (because the service layer expects DTO)
            var dto = new UpdatedDepartmentDto
            {
                Id = id.Value,//make sure to include the id to know which department to update
                //if thereis no id it will excpect it as u want to add new one 
                Name = vm.Name,
                code = vm.code,
                description = vm.description
            };

            //_departmentService.UpdateDepartment(dto);


            try
            {
                if (ModelState.IsValid)
                {
                    int res = _departmentService.UpdateDepartment(dto);
                    if (res > 0)
                    {
                        return RedirectToAction("Index"); //if its valid return to the same page 

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "dept cant be created");
                        return View(vm);
                    }

                }
                else
                    return View(vm); //give the same data in the inputs and gives the error msg 
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
                return View(vm);
            }

        }

        public IActionResult Delete([FromRoute]int? id)
        {
            if (id is null) return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null) return NotFound();

            return View(department);


        }

        [HttpPost]
        //fucking hard delete 
        public IActionResult Delete([FromRoute] int id)
        {
            var message = string.Empty;
            try
            {
                var isDeleted = _departmentService.DeleteDepartment(id);

                if ( isDeleted > 0 )
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Department is not deleted.");
                return View();
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

                return RedirectToAction("Index");
            }
        }

    }
}
