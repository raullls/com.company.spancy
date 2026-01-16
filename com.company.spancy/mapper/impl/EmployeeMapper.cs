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
    public class EmployeeMapper : BaseMapper<EmployeeDto, Employee>, IBaseMapper<EmployeeDto, Employee>
    {
        public EmployeeMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PermanentEmployeeDto, PermanentEmployee>();
                cfg.CreateMap<PermanentEmployee, PermanentEmployeeDto>();
                cfg.CreateMap<ContractorEmployeeDto, ContractorEmployee>();
                cfg.CreateMap<ContractorEmployee, ContractorEmployeeDto>();
                cfg.CreateMap<EmployeeDto, Employee>()
                    .ConvertUsing((src, dst) =>
                    {
                        if (src is PermanentEmployeeDto)
                        {
                            return this.mapper.Map(src, new PermanentEmployee());
                        }
                        else
                        {
                            return this.mapper.Map(src, new ContractorEmployee());
                        }
                    });
                cfg.CreateMap<Employee, EmployeeDto>()
                    .ConvertUsing((src, dst) =>
                    {
                        if (src is PermanentEmployee)
                        {
                            PermanentEmployeeDto permanentEmployeeDto = this.mapper.Map(src, new PermanentEmployeeDto());
                            permanentEmployeeDto.Type = "permanentEmployee";
                            return permanentEmployeeDto;
                        }
                        else
                        {
                            ContractorEmployeeDto contractorEmployeeDto = this.mapper.Map(src, new ContractorEmployeeDto());
                            contractorEmployeeDto.Type = "contractorEmployee";
                            return contractorEmployeeDto;
                        }
                    });
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
