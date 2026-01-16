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
    public class ItemService : BaseService<ItemDto, Item>, IItemService
    {
        public object CreateItemTX([Validated("itemValidator")] ItemDto dto)
        {
            return this.CreateTX(dto);
        }

        public object UpdateItemTX([Validated("itemValidator")] ItemDto dto)
        {
            return this.UpdateTX(dto);
        }
    }
}
