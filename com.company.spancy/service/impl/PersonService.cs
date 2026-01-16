using com.company.spancy.dto;
using com.company.spancy.entity;
using Spring.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service.impl
{
    public class PersonService : BaseService<PersonDto, Person>, IPersonService
    {
        public object CreatePersonTX([Validated("personValidator")] PersonDto dto)
        {
            return this.CreateTX(dto);
        }

        public object UpdatePersonTX([Validated("personValidator")] PersonDto dto)
        {
            return this.UpdateTX(dto);
        }
    }
}
