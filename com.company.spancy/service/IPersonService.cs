using com.company.spancy.dto;
using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service
{
    public interface IPersonService : IBaseService<PersonDto>
    {
        object UpdatePersonTX(PersonDto dto);
        object CreatePersonTX(PersonDto dto);
    }
}
