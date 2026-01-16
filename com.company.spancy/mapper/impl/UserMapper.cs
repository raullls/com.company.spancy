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
    public class UserMapper : BaseMapper<UserDto, User>, IBaseMapper<UserDto, User>
    {
        public UserMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, User>()
                    .AfterMap((src, dest) =>
                    {
                        dest.Groups = this.Dao.LoadEntity(src.Id)?.Groups;
                    });
                cfg.CreateMap<User, UserDto>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}