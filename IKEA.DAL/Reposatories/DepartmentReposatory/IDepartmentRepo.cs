using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Reposatories.DepartmentReposatory
{
    public interface IDepartmentRepo
    {
        public IEnumerable<Department> GetAll(bool withTracking=false);
        public Department GetById(int id);
        public int Add(Department department);
        public int Update(Department department);
        public int Delete(int id);


    }
}
