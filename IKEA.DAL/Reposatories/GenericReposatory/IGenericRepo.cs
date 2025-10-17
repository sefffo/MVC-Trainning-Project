using IKEA.DAL.Models;
using IKEA.DAL.Models.Employee;
using IKEA.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Reposatories.GenericReposatory
{
    public interface IGenericRepo<TEntity> where TEntity : BaseEntity //3shan tkon daymn mota2ekd enho 3 tool class in the database
    {
        public IQueryable<TEntity> GetAll(bool withTracking = false);
        public TEntity GetById(int id);
        public void Add(TEntity item);
        public void Update(TEntity item);
        public void Delete(int id);


        public IEnumerable<TEntity> GetAllEnum();


        public IQueryable<TEntity> GetAllQuer();


    }
}
