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
    public class ClientMapper : BaseMapper<ClientDto, Client>, IBaseMapper<ClientDto, Client>
    {
        public ClientMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientDto, Client>()
                    .AfterMap((src, dest) =>
                    {
                        dest.Accounts = this.Dao.LoadEntity(src.Id)?.Accounts;
                    });
                cfg.CreateMap<Client, ClientDto>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
