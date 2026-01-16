using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class PermanentEmployee : Employee
    {
        public virtual int Leaves { get; set; }
        public virtual int Salary { get; set; }
    }
}
