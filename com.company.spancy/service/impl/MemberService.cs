using com.company.spancy.dao;
using com.company.spancy.dto;
using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service.impl
{
    public class MemberService : BaseService<MemberDto, Member>, IMemberService
    {
        public IMemberMemberService MemberMemberService { get; set; }
        public object RemoveMemberByIdTX(long id)
        {
            Member member = this.Dao.LoadEntity(id);
            foreach (Member m in member.Members.ToList())
            {
                MemberMemberDto memberMemberDto = new MemberMemberDto();
                memberMemberDto.MemberId1 = this.Mapper.Map(member);
                memberMemberDto.MemberId2 = this.Mapper.Map(m);
                this.MemberMemberService.RemoveTX(memberMemberDto);
            }
            foreach (Member m in member.ReverseMembers.ToList())
            {
                MemberMemberDto memberMemberDto = new MemberMemberDto();
                memberMemberDto.MemberId1 = this.Mapper.Map(m);
                memberMemberDto.MemberId2 = this.Mapper.Map(member);
                this.MemberMemberService.RemoveTX(memberMemberDto);
            }
            this.Dao.DeleteEntity(member);
            return 0;
        }
    }
}
