using com.company.spancy.dao;
using com.company.spancy.dto;
using com.company.spancy.entity;
using com.company.spancy.mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service.impl
{
    public class MemberMemberService : IMemberMemberService
    {
        public IMemberDao MemberDao { get; set; }
        public IMemberMemberDao MemberMemberDao { get; set; }
        public IBaseMapper<MemberDto,Member> MemberMapper { get; set; }

        public object CreateTX(MemberMemberDto dto)
        {
            Member member1 = this.MemberDao.LoadEntity(dto.MemberId1.Id);
            Member member2 = this.MemberDao.LoadEntity(dto.MemberId2.Id);

            if (member1.Members == null)
                member1.Members = new List<Member>();

            member1.Members.Add(member2);

            return this.MemberDao.SaveEntity(member1);
        }

        public IList<MemberMemberDto> FindAllRO()
        {
            IList<MemberMemberDto> memberMemberDtos = new List<MemberMemberDto>();
            IList<Member> memberList = this.MemberDao.LoadAllEntities();
            foreach (Member member in memberList)
            {
                MemberDto memberDto = this.MemberMapper.Map(member);
                foreach (Member member2 in member?.Members ?? new List<Member>())
                {
                    MemberMemberDto memberMemberDto = new MemberMemberDto();
                    memberMemberDto.MemberId1 = memberDto;
                    MemberDto memberDto2 = this.MemberMapper.Map(member2);
                    memberMemberDto.MemberId2 = memberDto2;
                    memberMemberDtos.Add(memberMemberDto);
                }
            }
            return memberMemberDtos;
        }

        public MemberMemberDto FindByIdRO(long id)
        {
            throw new NotImplementedException();
        }

        public object IsPresentRO(MemberMemberDto dto)
        {
            bool status = false;
            IList<Member> memberList = this.MemberMemberDao.IsPresent(dto.MemberId1.Id, dto.MemberId2.Id);
            if (memberList.Count > 0)
            {
                status = true;
            } 
            else
            {
                memberList = this.MemberMemberDao.IsPresent(dto.MemberId2.Id, dto.MemberId1.Id);
                if (memberList.Count > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public object RemoveByIdTX(long id)
        {
            throw new NotImplementedException();
        }

        public object RemoveTX(MemberMemberDto dto)
        {
            Member member1 = this.MemberDao.LoadEntity(dto.MemberId1.Id);
            Member member2 = this.MemberDao.LoadEntity(dto.MemberId2.Id);

            member1.Members.Remove(member2);
            this.MemberDao.UpdateEntity(member1);
            return 0;
        }

        public object UpdateTX(MemberMemberDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
