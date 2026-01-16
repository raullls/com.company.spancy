using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dao
{
    public interface IManuscriptAuthorDao : IBaseDao<ManuscriptAuthor>
    {
        IList<ManuscriptAuthor> IsPresent(long manuscriptId, long authorId);
    }
}
