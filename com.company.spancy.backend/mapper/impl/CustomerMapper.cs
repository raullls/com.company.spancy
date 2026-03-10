using AutoMapper;
using com.company.spancy.backend.dto;
using com.company.spancy.backend.entity;

namespace com.company.spancy.backend.mapper.impl
{
    public class CustomerMapper : BaseMapper<CustomerDto, Customer>, IBaseMapper<CustomerDto, Customer>
    {
        public CustomerMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CustomerDto, Customer>()
                    .ForMember(dest => dest.Cart, opt => opt.MapFrom(src => new Cart { Amount = src.Amount }));

                cfg.CreateMap<Customer, CustomerDto>()
                    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Cart != null ? src.Cart.Amount : 0));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
