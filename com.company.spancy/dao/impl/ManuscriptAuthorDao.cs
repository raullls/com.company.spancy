using com.company.spancy.entity;
using Spring.Data.NHibernate.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dao.impl
{
    public class ManuscriptAuthorDao : BaseDao<ManuscriptAuthor>, IManuscriptAuthorDao
    {
        public IList<ManuscriptAuthor> IsPresent(long manuscriptId, long authorId)
        {
            string hql = "select distinct ma from ManuscriptAuthor ma where ma.Manuscript.Id = ? and ma.Author.Id = ?";
            return HibernateTemplate.Find(hql, new object[] { manuscriptId, authorId }).Cast<ManuscriptAuthor>().ToList();
        }
    }
}
