using Common.Logging;
using NHibernate;
using Spring.Context;
using Spring.Context.Attributes;
using Spring.Data.NHibernate.Support;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace com.company.spancy.dao.impl
{
    public class BaseDao<T> : HibernateDaoSupport, IBaseDao<T>
    {
        public long SaveEntity(T obj)
        {
            return (long)HibernateTemplate.Save(obj);
        }

        public void DeleteEntity(T obj)
        {
            HibernateTemplate.Delete(obj);
        }

        public T LoadEntity(long id)
        {
            return (T)HibernateTemplate.Get(typeof(T), id);
        }

        public void UpdateEntity(T obj)
        {
            HibernateTemplate.Merge(obj);
        }

        public IList<T> LoadAllEntities()
        {
            return HibernateTemplate.LoadAll(typeof(T)).Cast<T>().ToList();
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
            return HibernateTemplate.FindByNamedParam(hql, dict.Keys.ToArray(), dict.Values.ToArray()).Cast<T>().ToList();
        }

        public void Evict(T obj)
        {
            HibernateTemplate.Evict(obj);
        }
    }
}
