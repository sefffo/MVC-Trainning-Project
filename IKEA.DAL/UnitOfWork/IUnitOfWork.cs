using IKEA.DAL.Reposatories.DepartmentReposatory;
using IKEA.DAL.Reposatories.EmployeeReposatory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IEmployeeRepo employeeRepo { get; set; }
        public IDepartmentRepo departmentRepo { get; set; }


        public int Complete();

    }
}
