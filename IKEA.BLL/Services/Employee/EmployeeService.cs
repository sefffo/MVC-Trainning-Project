using AutoMapper;
using IKEA.BLL.Dto_s.EmployeeDto_s;
using IKEA.DAL.Models.Employee;
using IKEA.DAL.Reposatories.EmployeeReposatory;
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
        public IEmployeeRepo _EmpRepo { get; }
        public IMapper Mapper { get; }

        public EmployeeService(IEmployeeRepo EmpRepo, IMapper mapper)
        {
            _EmpRepo = EmpRepo;
            Mapper = mapper;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var Employees = _EmpRepo.GetAll().ToList();//bmshy haly 
            var mappedEmployees = Mapper.Map<IEnumerable<IKEA.DAL.Models.Employee.Employee>, IEnumerable<EmployeeDto>>(Employees);//ostor yarb
            return mappedEmployees;

            //var Result = _EmpRepo.GetAllEnum().Where(x => x.isDeleted != true).Select(e => new EmployeeDto
            //{
            //    Id = e.id,
            //    Name = e.Name,
            //    Age = e.Age,
            //}); //the where will run in the excution time it will filter after it feactches all the employeees 
            //var Result = _EmpRepo.GetAllQuer().Where(x => x.isDeleted != true).Select(e => new EmployeeDto
            //{
            //    Id = e.id,
            //    Name = e.Name,
            //    Age = e.Age,
            //});
            //return Result.ToList(); 
        }

        public EmployeeDetailsDto GetEmployeeById(int id)
        {
            var Employe = _EmpRepo.GetById(id);
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

            var res = _EmpRepo.Add(emp);
            return res;
        }

        public int UpdateEmployee(UpdateEmployeeDto dto)
        {
            var emp = Mapper.Map<UpdateEmployeeDto, IKEA.DAL.Models.Employee.Employee>(dto);

            emp.updatedBy = 1;
            emp.UpdatedOn = DateTime.Now;

            var res = _EmpRepo.Update(emp);
            return res;
        }

        public int DeleteEmployee(int? id)
        {
            if (id is not null)
            {
                return _EmpRepo.Delete(id.Value);

            }
            else return 0;
        }

        public IEnumerable<EmployeeDto> GetEmployees(string? searchValue)
        {


            var Employees = _EmpRepo.GetAll(searchValue).ToList();//bmshy haly 
            var mappedEmployees = Mapper.Map<IEnumerable<IKEA.DAL.Models.Employee.Employee>, IEnumerable<EmployeeDto>>(Employees);//ostor yarb
            return mappedEmployees;



        }
    }
}
