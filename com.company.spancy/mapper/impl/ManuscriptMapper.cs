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
    public class ManuscriptMapper : BaseMapper<ManuscriptDto, Manuscript>, IBaseMapper<ManuscriptDto, Manuscript>
    {
        public ManuscriptMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ManuscriptDto, Manuscript>()
                    .AfterMap((src, dest) =>
                    {
                        dest.ManuscriptAuthors = this.Dao.LoadEntity(src.Id)?.ManuscriptAuthors;
                    });
                cfg.CreateMap<Manuscript, ManuscriptDto>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
