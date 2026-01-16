using AutoMapper;
using com.company.spancy.dto;
using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.mapper.impl
{
    public class MemberMapper : BaseMapper<MemberDto, Member>, IBaseMapper<MemberDto, Member>
    {
        public MemberMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MemberDto, Member>()
                    .AfterMap((src, dest) =>
                    {
                        dest.Members = this.Dao.LoadEntity(src.Id)?.Members;
                        dest.ReverseMembers = this.Dao.LoadEntity(src.Id)?.ReverseMembers;
                    });
                cfg.CreateMap<Member, MemberDto>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
