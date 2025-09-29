using IKEA.BLL.Dto_s.DepartmentsDto_s;
using IKEA.DAL.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Factories.DepartmentFactory
{
    public static class DepartmentFact
    {
     
        public static DepartmentDto ToDepartmentDto (this Department department)
                                                    //It tells the compiler: "Pretend this static method belongs to the Department class."
        {
            return new DepartmentDto()
            {
                Id = department.id,
                Name = department.Name,
                description = department.Description,
                code = department.Code
            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto(department);//using the constructor
        }

        //the opposite of the above method

        public static Department ToDepartment (this CreateDepartmentDto departmentDto)
        {
            return new Department()
            {
                Name = departmentDto.Name,
                Description = departmentDto.description,
                Code = departmentDto.code,
                createdBy= 1,
                CreatedOn= DateTime.Now,
                updatedBy= 1,
                UpdatedOn= DateTime.Now,
                isDeleted = false
            };
        }

        public static Department FromUpdatedDeptDtoToDepartment(this UpdatedDepartmentDto departmentDto)
        {
            return new Department()
            {
                id = departmentDto.Id,
                //important to set the id for update operation bec it gonna be sent from the frot end so we need to know which one to update
                Name = departmentDto.Name,
                Description = departmentDto.description,
                Code = departmentDto.code,
                updatedBy = 1,
                UpdatedOn = DateTime.Now
            };
        }



    }
}
