using IKEA.BLL.Dto_s.DepartmentsDto_s;
using IKEA.BLL.Factories.DepartmentFactory;
using IKEA.DAL.Reposatories.DepartmentReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Department
{
    public class DepartmentServices:IDepartmentService
    {
        //u must get the repo in the service and we cant do that manully so we use dependency injection

        //DepartmentRepo d = new DepartmentRepo(); => because u created a pattern so u can get the repo directly

        private readonly IDepartmentRepo _DeptRepo;
        public DepartmentServices(IDepartmentRepo DeptRepo)//inject the repo so the clr will create it for us
        {
            _DeptRepo = DeptRepo;
        }
        // Controller => service => repo => context (Mbd2 sabbbbeeeeeeetttt)


        //3 7ASAB EL CLIENT M7TAG EH YT3ERD bussiness logic 3la department
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
             
           var departments = _DeptRepo.GetAll();
            // manually mapping
            //var mapppedDepartments = departments.Select(d => new DepartmentDto
            //{
            //    Id = d.id,
            //    Name = d.Name,
            //    description = d.Description,
            //    code = d.Code
            //}).ToList();

            //using extension method from the factory
            
            
            //var mapppedDepartments = departments.Select(d => d.ToDepartmentDto()).ToList();

            List<DepartmentDto> mapppedDepartments = new List<DepartmentDto>(); //slightly faster than using linq
            foreach (var dept in departments)
            {
                var MapppedDepart = dept.ToDepartmentDto();
                mapppedDepartments.Add(MapppedDepart);
            }
            return mapppedDepartments;
        }


        //another method to get by id with another context to be showen in the views depending on the id for another bussiness logic
        public DepartmentDetailsDto GetDepartmentById(int id)
        {

            var department = _DeptRepo.GetById(id);

            if(department is null)
            {
                return null;
            }
            else
            {
                //using  copy constructor
                var mappedDepartment = new DepartmentDetailsDto(department);
                return mappedDepartment;

            }


        }


        public int AddDepartment(CreateDepartmentDto department )
        {
            var dept = department.ToDepartment();//using the opposite method in the factory
            return _DeptRepo.Add(dept); // return the id of the added department how many rows affected

        }

        public int UpdateDepartment(UpdatedDepartmentDto department)
        {
            var dept = department.FromUpdatedDeptDtoToDepartment();//using the opposite method in the factory
            return _DeptRepo.Update(dept); // return the id of the added department and how many rows affected
        }
        public int DeleteDepartment(int id)
        {
            return _DeptRepo.Delete(id); // return the id of the added department and how many rows affected
        }
    }
}
