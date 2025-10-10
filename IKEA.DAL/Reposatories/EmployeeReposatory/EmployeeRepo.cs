using IKEA.DAL.Contexts;
using IKEA.DAL.Models.Employee;
using IKEA.DAL.Reposatories.GenericReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Reposatories.EmployeeReposatory
{
    public class EmployeeRepo : GenericRepo<Employee>, IEmployeeRepo 
                                //3mlna zy kobry mn el generic ll employee 3shan lw fe hagat gdeda fe el emp 
    {
        //we dont make the context as a field in the class to avoid memory leaks
        //we use dependency injection in the service layer to manage the context life cycle
        //clr is the one who create the instance of the context

        // 3shan el repo da zy elevator ya5ood el context mn el service layer w2t el est5dam

        private readonly APPDbContext _context;

        public EmployeeRepo(APPDbContext Context):base(Context)
            //inject the context
        {
            _context = Context;
        }

    }
}
