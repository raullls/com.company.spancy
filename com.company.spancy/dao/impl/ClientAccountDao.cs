using com.company.spancy.entity;
using Spring.Data.NHibernate.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dao.impl
{
    public class ClientAccountDao : HibernateDaoSupport, IClientAccountDao
    {
        public IList<Client> GetAll()
        {
            string hql = "select distinct c from Client c join c.Accounts a";
            return HibernateTemplate.Find(hql).Cast<Client>().ToList();
        }

        public IList<Client> IsPresent(long clientId, long accountId)
        {
            string hql = "select distinct c from Client c join c.Accounts a where c.Id = ? and a.Id = ?";
            return HibernateTemplate.Find(hql, new object[] { clientId, accountId }).Cast<Client>().ToList();
        }
    }
}
