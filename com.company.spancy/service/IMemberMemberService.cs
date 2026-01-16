using com.company.spancy.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service
{
    public interface IMemberMemberService : IBaseService<MemberMemberDto>
    {
        object RemoveTX(MemberMemberDto dto);
        object IsPresentRO(MemberMemberDto dto);
    }
}
