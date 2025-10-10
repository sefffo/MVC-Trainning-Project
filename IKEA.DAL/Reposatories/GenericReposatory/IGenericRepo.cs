using IKEA.DAL.Models;
using IKEA.DAL.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Reposatories.GenericReposatory
{
    public interface IGenericRepo<TEntity> where TEntity : BaseEntity //3shan tkon daymn mota2ekd enho 3 tool class in the database
    {
        public IEnumerable<TEntity> GetAll(bool withTracking = false);
        public TEntity GetById(int id);
        public int Add(TEntity item);
        public int Update(TEntity item);
        public int Delete(int id);
    }
}
