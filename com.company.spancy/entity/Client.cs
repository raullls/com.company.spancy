using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class Client
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Account> Accounts { get; set; } = new List<Account>();
    }
}
