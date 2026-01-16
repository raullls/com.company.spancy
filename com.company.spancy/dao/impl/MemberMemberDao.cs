using com.company.spancy.entity;
using Spring.Data.NHibernate.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dao.impl
{
    public class MemberMemberDao : HibernateDaoSupport, IMemberMemberDao
    {
        public IList<Member> GetAll()
        {
            string hql = "select distinct m from Member m join m.Members ms";
            return HibernateTemplate.Find(hql).Cast<Member>().ToList();
        }

        public IList<Member> IsPresent(long member1Id, long member2Id)
        {
            string hql = "select distinct m from Member m join m.Members ms where m.Id = ? and ms.Id = ?";
            return HibernateTemplate.Find(hql, new object[] { member1Id, member2Id }).Cast<Member>().ToList();
        }
    }
}
