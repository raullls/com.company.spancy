using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class Cart
    {
        public virtual long Id { get; set; }
        public virtual double Amount { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
