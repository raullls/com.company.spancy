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
    public class AccountMapper : BaseMapper<AccountDto, Account>, IBaseMapper<AccountDto, Account>
    {
        public AccountMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountDto, Account>()
                    .AfterMap((src, dest) =>
                        {
                            dest.Clients = this.Dao.LoadEntity(src.Id)?.Clients;
                        });
                cfg.CreateMap<Account, AccountDto>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
