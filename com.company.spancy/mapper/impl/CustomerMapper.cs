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
