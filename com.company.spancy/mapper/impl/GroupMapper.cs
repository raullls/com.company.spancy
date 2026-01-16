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
    public class GroupMapper : BaseMapper<GroupDto, Group>, IBaseMapper<GroupDto, Group>
    {
        public GroupMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GroupDto, Group>();
                cfg.CreateMap<Group, GroupDto>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
