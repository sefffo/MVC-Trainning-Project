//using IKEA.DAL.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  IKEA.DAL.Models.Employee;

namespace IKEA.DAL.Models.Department
{
    public class Department:BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Code { get; set; }


        public virtual ICollection<IKEA.DAL.Models.Employee.Employee> Employees { get; set; } = new HashSet<IKEA.DAL.Models.Employee.Employee>();

    }
}
