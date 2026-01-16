using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class Account
    {
        public Account()
        {
            this.Clients = new List<Client>();
        }
        public virtual long Id { get; set; }
        public virtual string Number { get; set; }
        public virtual IList<Client> Clients { get; set; }
    }
}
