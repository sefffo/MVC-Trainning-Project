using IKEA.DAL.Contexts;
using IKEA.DAL.Reposatories.GenericReposatory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Reposatories.DepartmentReposatory
{
    public class DepartmentRepo: GenericRepo<Department>,IDepartmentRepo
    {
        //we dont make the context as a field in the class to avoid memory leaks
        //we use dependency injection in the service layer to manage the context life cycle
        //clr is the one who create the instance of the context

        // 3shan el repo da zy elevator ya5ood el context mn el service layer w2t el est5dam

        private readonly APPDbContext _context;

        public DepartmentRepo(APPDbContext Context):base(Context) //oop serffff enta lazem t3rf elchild eno yshof el parent 
            //inject the context
        {
            _context = Context;
        }
        public IEnumerable<Department> GetAll(bool withTracking = false)//by default without tracking
        {
            if(withTracking==true)
            {
                return _context.Departments.ToList();
            }
            else 
            {
                return _context.Departments.AsNoTracking().ToList();
            }
        }

        public Department GetById(int id)
        {
            var department = _context.Departments.Find(id);
            return department;
        }

        public int Add(Department department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges();//return number of affected rows
        }

        public int Update(Department department)
        {
            _context.Departments.Update(department);
            return _context.SaveChanges();//return number of affected rows
        }

        public int Delete(int id)
        {
            
            _context.Departments.Remove(GetById(id));
            return _context.SaveChanges();//return number of affected rows
        }



       

      

        //public IEnumerable<Department> GetAllDepartments()
        //{
        //    var departments = _context.Departments.ToList();    
        //    return departments;
        //}
    }
}
