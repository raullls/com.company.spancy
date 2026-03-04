namespace com.company.spancy.backend.dao
{
    public interface IBaseDao<T>
    {
        long SaveEntity(T obj);
        void UpdateEntity(T obj);
        void DeleteEntity(T obj);
        T LoadEntity(object id);
        IList<T> LoadAllEntities();
        IList<T> FindByValueObject(string hql, T obj);
        void Evict(T obj);
    }
}