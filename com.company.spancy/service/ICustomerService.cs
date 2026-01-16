using com.company.spancy.dto;
using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service
{
    public interface ICustomerService : IBaseService<CustomerDto>
    {
        object CreateCustomerTX(CustomerDto dto);
        object UpdateCustomerTX(CustomerDto dto);
    }
}
