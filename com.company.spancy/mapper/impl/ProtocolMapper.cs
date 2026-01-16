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
    public class ProtocolMapper : BaseMapper<ProtocolDto,Protocol>, IBaseMapper<ProtocolDto,Protocol> 
    {
        public ProtocolMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TcpDto, Tcp>();
                cfg.CreateMap<Tcp, TcpDto>();
                cfg.CreateMap<SnmpDto, Snmp>();
                cfg.CreateMap<Snmp, SnmpDto>();
                cfg.CreateMap<ProtocolDto, Protocol>()
                    .ConvertUsing((src, dst) =>
                    {
                        if (src is TcpDto)
                        {
                            return this.mapper.Map(src, new Tcp());
                        }
                        else
                        {
                            return this.mapper.Map(src, new Snmp());
                        }
                    });
                cfg.CreateMap<Protocol, ProtocolDto>()
                    .AfterMap((src, dst) =>
                    {
                        dst.Type = src is Tcp ? "tcp" : "snmp";
                    });
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
