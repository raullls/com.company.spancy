using com.company.spancy.entity;
using NHibernate;
using Spring.Data.NHibernate.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dao.impl
{
    public class UserGroupDao : HibernateDaoSupport, IUserGroupDao
    {
        public IList<User> GetAll()
        {
            return HibernateTemplate.Find("select distinct u from User u join u.Groups g").Cast<User>().ToList();
        }

        public IList<User> IsPresent(long userId, long groupId)
        {
            string hql = "select distinct u from User u join u.Groups g where u.Id = ? and g.Id = ?";
            return HibernateTemplate.Find(hql, new object[] { userId, groupId }).Cast<User>().ToList();
        }
    }
}
