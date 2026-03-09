using AutoMapper;
using com.company.spancy.backend.dto;
using com.company.spancy.backend.entity;

namespace com.company.spancy.backend.mapper.impl
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