using System.Text.RegularExpressions;
using Spring.Data.NHibernate.Support;

namespace com.company.spancy.backend.dao.impl
{
    public class BaseDao<T> : HibernateDaoSupport, IBaseDao<T>
    {
        public long SaveEntity(T obj)
        {
            return (long)this.HibernateTemplate.Save(obj);
        }

        public void UpdateEntity(T obj)
        {
            this.HibernateTemplate.Merge(obj);
        }

        public void DeleteEntity(T obj)
        {
            this.HibernateTemplate.Delete(obj);
        }

        public T LoadEntity(object id)
        {
            return (T)this.HibernateTemplate.Get(typeof(T), id);
        }

        public IList<T> LoadAllEntities()
        {
            return this.HibernateTemplate.LoadAll(typeof(T)).Cast<T>().ToList();
        }

        public IList<T> FindByValueObject(string hql, T obj)
        {
            IDictionary<string, object> dict = new Dictionary<string, object>();
            foreach (Match match in Regex.Matches(hql, @"(?<=:)[A-Za-z0-9_\.]+"))
            {
                hql = hql.Replace(":" + match.Value, ":" + match.Value.Replace(".", ""));
                object current = obj;
                foreach (string part in match.Value.Split('.'))
                {
                    if (current == null) break;  
                    current = current.GetType().GetProperty(part)?.GetValue(current);
                }
                dict[match.Value.Replace(".", "")] = current;
            }
            return this.HibernateTemplate.FindByNamedParam(hql, dict.Keys.ToArray(), dict.Values.ToArray()).Cast<T>().ToList();
        }

        public void Evict(T obj)
        {
            this.HibernateTemplate.Evict(obj);
        }
    }
}