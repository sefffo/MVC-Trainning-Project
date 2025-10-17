using IKEA.BLL.Dto_s.DepartmentsDto_s;
using IKEA.BLL.Factories.DepartmentFactory;
using IKEA.DAL.Reposatories.DepartmentReposatory;
using IKEA.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Department
{
    public class DepartmentServices:IDepartmentService
    {
        private readonly IUnitOfWork unitOfWork;

        //u must get the repo in the service and we cant do that manully so we use dependency injection

        //DepartmentRepo d = new DepartmentRepo(); => because u created a pattern so u can get the repo directly


        public DepartmentServices(IUnitOfWork unitOfWork)//inject the repo so the clr will create it for us
        {
            this.unitOfWork = unitOfWork;
            //unitOfWork = DeptRepo;
        }
        // Controller => service => repo => context (Mbd2 sabbbbeeeeeeetttt)


        //3 7ASAB EL CLIENT M7TAG EH YT3ERD bussiness logic 3la department
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
             
           var departments = unitOfWork.departmentRepo.GetAll().ToList();
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

            var department = unitOfWork.departmentRepo.GetById(id);

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
             unitOfWork.departmentRepo.Add(dept); // return the id of the added department how many rows affected
            return unitOfWork.Complete();
        }

        public int UpdateDepartment(UpdatedDepartmentDto department)
        {
            var dept = department.FromUpdatedDeptDtoToDepartment();//using the opposite method in the factory
            unitOfWork.departmentRepo.Update(dept); // return the id of the added department and how many rows affected
            return unitOfWork.Complete();
        }
        public int DeleteDepartment(int id)
        {
             unitOfWork.departmentRepo.Delete(id);
            return unitOfWork.Complete(); // return the id of the added department and how many rows affected
        }
    }
}
