using IKEA.DAL.Contexts;
using IKEA.DAL.Reposatories.DepartmentReposatory;
using IKEA.DAL.Reposatories.EmployeeReposatory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.UnitOfWork
{
    public class UOW : IUnitOfWork
    {
        private readonly APPDbContext context;

        public UOW(APPDbContext context)
        {
            this.context = context;
            //3shan lazem lw geet aklm el uow 3shan st5dm el repo f aklem el context 
            employeeRepo = new EmployeeRepo(context);
            departmentRepo = new DepartmentRepo(context);
        }

        public IEmployeeRepo employeeRepo { get ; set; }
        public IDepartmentRepo departmentRepo { get ; set; }

        public int Complete()
        {
            return context.SaveChanges();
        }
        //fe nas bt7ot fun despose w dah msh sa7777 3shan e7na bnst5dm dependancy injection
    }
}
