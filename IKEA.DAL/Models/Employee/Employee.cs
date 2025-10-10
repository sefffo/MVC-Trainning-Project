using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

    }
}
