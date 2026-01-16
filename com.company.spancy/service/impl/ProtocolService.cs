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
    public class ProtocolService : BaseService<ProtocolDto,Protocol>, IProtocolService
    {
        public object CreateProtocolTX([Validated("protocolValidator")] ProtocolDto protocolDto)
        {
            return this.CreateTX(protocolDto);
        }

        public object UpdateProtocolTX([Validated("protocolValidator")] ProtocolDto protocolDto)
        {
            return this.UpdateTX(protocolDto);
        }
    }
}
