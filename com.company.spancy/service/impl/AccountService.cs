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
    public class AccountService : BaseService<AccountDto, Account>, IAccountService
    {
        public object CreateAccountTX([Validated("accountValidator")] AccountDto accountDto)
        {
            return base.CreateTX(accountDto);
        }

        public object UpdateAccountTX([Validated("accountValidator")] AccountDto accountDto)
        {
            return base.UpdateTX(accountDto);
        }
    }
}
