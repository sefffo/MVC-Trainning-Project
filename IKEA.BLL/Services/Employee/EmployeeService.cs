using AutoMapper;
using IKEA.BLL.Common.Servicies.Attachments;
using IKEA.BLL.Dto_s.EmployeeDto_s;
using IKEA.DAL.Models.Employee;
using IKEA.DAL.Reposatories.EmployeeReposatory;
using IKEA.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using IKEA.DAL.Models.Employee;

namespace IKEA.BLL.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAttachmentService attachmentService;

        public IUnitOfWork unitOfWork { get; }
        public IMapper Mapper { get; }

        public EmployeeService(IUnitOfWork EmpRepo, IMapper mapper, IAttachmentService attachmentService)
        {
            unitOfWork = EmpRepo;
            Mapper = mapper;
            this.attachmentService = attachmentService;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var Employees = unitOfWork.employeeRepo.GetAll().ToList();//bmshy haly 
            var mappedEmployees = Mapper.Map<IEnumerable<IKEA.DAL.Models.Employee.Employee>, IEnumerable<EmployeeDto>>(Employees);//ostor yarb
            return mappedEmployees;

            //var Result = unitOfWork.GetAllEnum().Where(x => x.isDeleted != true).Select(e => new EmployeeDto
            //{
            //    Id = e.id,
            //    Name = e.Name,
            //    Age = e.Age,
            //}); //the where will run in the excution time it will filter after it feactches all the employeees 
            //var Result = unitOfWork.GetAllQuer().Where(x => x.isDeleted != true).Select(e => new EmployeeDto
            //{
            //    Id = e.id,
            //    Name = e.Name,
            //    Age = e.Age,
            //});
            //return Result.ToList(); 
        }

        public EmployeeDetailsDto GetEmployeeById(int id)
        {
            var Employe = unitOfWork.employeeRepo.GetById(id);
            var mappedEmployee = Mapper.Map<IKEA.DAL.Models.Employee.Employee, EmployeeDetailsDto>(Employe);
            return mappedEmployee;
        }

        public int AddEmployee(CreateEmployeeDto dto)
        {
            var emp = Mapper.Map<CreateEmployeeDto, IKEA.DAL.Models.Employee.Employee>(dto);

            if (dto.image is not null)
            {
                emp.ImageName = attachmentService.Upload(dto.image, "images");
            }

            emp.updatedBy = 1;
            emp.createdBy = 1;
            emp.CreatedOn = DateTime.Now;
            emp.UpdatedOn = DateTime.Now;

            unitOfWork.employeeRepo.Add(emp);
            return unitOfWork.Complete();
        }


        public int UpdateEmployee(UpdateEmployeeDto dto)
        {
            // 1️ Get existing entity from DB
            var existingEmp = unitOfWork.employeeRepo.GetById(dto.Id);
            if (existingEmp == null)
                return 0;

            // 2️ Map only changed fields onto the existing entity
            Mapper.Map(dto, existingEmp);

            // 3️ Handle image if provided
            //if (dto.image is not null)
            //{
            //    if (!string.IsNullOrEmpty(existingEmp.ImageName))
            //    {
            //        var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", existingEmp.ImageName);
            //        attachmentService.Delete(filepath);
            //    }

            //    existingEmp.ImageName = attachmentService.Upload(dto.image, "images");
            //}

            // 4️ Update audit fields
            existingEmp.updatedBy = 1;
            existingEmp.UpdatedOn = DateTime.Now;

            // 5️ Save changes
            unitOfWork.employeeRepo.Update(existingEmp);
            return unitOfWork.Complete();
        }


        //public int UpdateEmployee(UpdateEmployeeDto dto)
        //{
        //    var emp = Mapper.Map<UpdateEmployeeDto, IKEA.DAL.Models.Employee.Employee>(dto);

        //    //if (dto.image is not null)
        //    //{
        //    //    if (emp.ImageName is not null)
        //    //    {
        //    //        var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", emp.ImageName);
        //    //        attachmentService.Delete(filepath);

        //    //    }
        //    //    emp.ImageName = attachmentService.Upload(dto.image, "images");
        //    //}


        //    emp.updatedBy = 1;
        //    emp.UpdatedOn = DateTime.Now;

        //    unitOfWork.employeeRepo.Update(emp);
        //    return unitOfWork.Complete();
        //}

        public int DeleteEmployee(int? id)
        {
            var employee = unitOfWork.employeeRepo.GetById(id.Value);

            if (id is not null)
            {

                if(employee.ImageName is not null)
                {
                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", employee.ImageName);
                    attachmentService.Delete(filepath);

                }


                unitOfWork.employeeRepo.Delete(id.Value);
                return unitOfWork.Complete();
            }
            else return 0;
        }

        public IEnumerable<EmployeeDto> GetEmployees(string? searchValue)
        {


            var Employees = unitOfWork.employeeRepo.GetAll(searchValue).ToList();//bmshy haly 
            var mappedEmployees = Mapper.Map<IEnumerable<IKEA.DAL.Models.Employee.Employee>, IEnumerable<EmployeeDto>>(Employees);//ostor yarb
            return mappedEmployees;



        }
    }
}
