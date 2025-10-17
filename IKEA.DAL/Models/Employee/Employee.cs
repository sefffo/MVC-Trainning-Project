using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Models.Department;

namespace IKEA.DAL.Models.Employee
{
    public class Employee:BaseEntity
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public string? address { get; set; }
        public bool isActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int Salary { get; set; }
        public DateOnly HiringDate { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public Gender Gender { get; set; }

        public virtual IKEA.DAL.Models.Department.Department Department { get; set; } //ref el relation


        // el 3rd bta3o

        public  int? DepartmentID { get; set; }




        //attachments 

        public string ImageName {  get; set; }




    }
}
