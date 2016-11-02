using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletApi.DataLayer;

namespace WalletApi.Repositories.Generic
{
    public interface IRepository<T> where T : class,IEntity,new() 
    {
        int Add(T entity);

        IEnumerable<T> GetAll();

        T Get(int entityId);

        IEnumerable<T> GetFiltered(Func<T, bool> filter);
        
        void Update(T entity);
    }
}
