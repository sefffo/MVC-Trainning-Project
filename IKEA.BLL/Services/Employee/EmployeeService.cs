using AutoMapper;
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
        public IUnitOfWork unitOfWork { get; }
        public IMapper Mapper { get; }

        public EmployeeService(IUnitOfWork EmpRepo, IMapper mapper)
        {
            unitOfWork = EmpRepo;
            Mapper = mapper;
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

            emp.updatedBy = 1;
            emp.createdBy = 1;
            emp.CreatedOn = DateTime.Now;
            emp.UpdatedOn = DateTime.Now;

            unitOfWork.employeeRepo.Add(emp);
            return unitOfWork.Complete();
        }

        public int UpdateEmployee(UpdateEmployeeDto dto)
        {
            var emp = Mapper.Map<UpdateEmployeeDto, IKEA.DAL.Models.Employee.Employee>(dto);

            emp.updatedBy = 1;
            emp.UpdatedOn = DateTime.Now;

           unitOfWork.employeeRepo.Update(emp);
            return unitOfWork.Complete();
        }

        public int DeleteEmployee(int? id)
        {
            if (id is not null)
            {
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
