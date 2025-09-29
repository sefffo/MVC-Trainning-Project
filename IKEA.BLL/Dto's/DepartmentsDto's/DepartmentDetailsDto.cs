using IKEA.DAL.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Dto_s.DepartmentsDto_s
{

    public class DepartmentDetailsDto
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public string? description { get; set; }
        public string code { get; set; }
        public DateOnly CreatedOn { get; set; }
        public int createdBy { get; set; }
        public DateOnly UpdatedOn { get; set; }
        public int updatedBy { get; set; }


        public DepartmentDetailsDto(Department department  ) 
        {


            Id = department.id;
            Name = department.Name;
            description = department.Description;
            code = department.Code;
            CreatedOn = DateOnly.FromDateTime(department.CreatedOn);
            createdBy = department.createdBy;
            UpdatedOn = DateOnly.FromDateTime(department.UpdatedOn);
            updatedBy = department.updatedBy;



        }

    }
}
