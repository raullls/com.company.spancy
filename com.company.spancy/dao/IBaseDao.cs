using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dao
{
    public interface IBaseDao<T>
    {
        long SaveEntity(T obj);
        void DeleteEntity(T obj);
        T LoadEntity(long id);
        IList<T> LoadAllEntities();
        void UpdateEntity(T obj);
        IList<T> FindByValueObject(string hql, T obj);
        void Evict(T obj);
    }
}
