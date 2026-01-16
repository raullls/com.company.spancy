using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dao
{
    public interface IMemberMemberDao
    {
        IList<Member> GetAll();
        IList<Member> IsPresent(long member1Id, long member2Id);
    }
}
