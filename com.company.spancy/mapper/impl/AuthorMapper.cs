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
    public class AuthorMapper : BaseMapper<AuthorDto, Author>, IBaseMapper<AuthorDto, Author>
    {
        public AuthorMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AuthorDto, Author>()
                    .AfterMap((src, dest) =>
                    {
                        dest.ManuscriptAuthors = this.Dao.LoadEntity(src.Id)?.ManuscriptAuthors;
                    });
                cfg.CreateMap<Author, AuthorDto>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
