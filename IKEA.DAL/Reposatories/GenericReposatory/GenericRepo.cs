using IKEA.DAL.Contexts;
using IKEA.DAL.Models;
using IKEA.DAL.Models.Employee;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Reposatories.GenericReposatory
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : BaseEntity
    {
        private readonly APPDbContext _context;

        public GenericRepo(APPDbContext context) 
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking == true)
            {
                return _context.Set<TEntity>().ToList();
            }
            else
            {
                return _context.Set<TEntity>().AsNoTracking().ToList();
            }
        }

        public TEntity GetById(int id)
        {
            var item = _context.Set<TEntity>().Find(id);
            return item;
        }
        public int Add(TEntity item)
        {
            _context.Set<TEntity>().Add(item);
            return _context.SaveChanges();//return number of affected rows
        }

        public int Update(TEntity item)
        {
            _context.Set<TEntity>().Update(item);
            return _context.SaveChanges();//return number of affected rows
        }

        public int Delete(int id)
        {
            _context.Set<TEntity>().Remove(GetById(id));
            return _context.SaveChanges();//return number of affected rows
        }




    }
}
