using IKEA.DAL.Reposatories.DepartmentReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services
{
    public class DepartmentServices
    {
        //u must get the repo in the service and we cant do that manully so we use dependency injection

        //DepartmentRepo d = new DepartmentRepo(); => because u created a pattern so u can get the repo directly

        private readonly IDepartmentRepo _DeptRepo;
        public DepartmentServices(IDepartmentRepo DeptRepo)//inject the repo so the clr will create it for us
        {
            _DeptRepo = DeptRepo;
        }
        // Controller => service => repo => context (Mbd2 sabbbbeeeeeeetttt)
    }
}
