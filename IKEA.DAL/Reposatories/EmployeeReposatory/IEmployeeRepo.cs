using IKEA.DAL.Models.Employee;
using IKEA.DAL.Reposatories.GenericReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Reposatories.EmployeeReposatory
{
    public interface IEmployeeRepo:IGenericRepo<Employee>
    {
        public IEnumerable<Employee> GetAll(string? seacrValue);
    }

   
}
