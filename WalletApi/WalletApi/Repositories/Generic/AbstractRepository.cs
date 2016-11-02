using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.DataLayer;

namespace WalletApi.Repositories.Generic
{
    public abstract class AbstractRepository<T> : 
        IRepository<T> where T : class,IEntity,new()
    {
        WalletApiDBEntities _db = new WalletApiDBEntities();

        public int Add(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
            return entity.Id;
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>();
        }

        public T Get(int entityId)
        {
            return _db.Set<T>().Find(entityId);
        }

        public IEnumerable<T> GetFiltered(Func<T,bool> filter)
        {
            return _db.Set<T>().Where(filter);
        }

        public void Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
