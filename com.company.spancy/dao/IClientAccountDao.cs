using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dao
{
    public interface IClientAccountDao
    {
        IList<Client> GetAll();
        IList<Client> IsPresent(long clientId, long accountId);
    }
}
