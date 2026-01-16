using com.company.spancy.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service
{
    public interface IAccountService : IBaseService<AccountDto>
    {
        object CreateAccountTX(AccountDto accountDto);
        object UpdateAccountTX(AccountDto accountDto);
    }
}
