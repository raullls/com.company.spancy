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
    public class BookMapper : BaseMapper<BookDto, Book>, IBaseMapper<BookDto, Book>
    {
        public BookMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BookDto, Book>()
                    .ForMember(dest => dest.Shipping, opt => opt.MapFrom(src => new Shipping { City = src.City }));

                cfg.CreateMap<Book, BookDto>()
                    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Shipping != null ? src.Shipping.City : null));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
