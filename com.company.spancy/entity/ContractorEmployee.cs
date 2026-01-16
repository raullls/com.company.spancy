using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class ContractorEmployee : Employee
    {
        public virtual int HourleyRate { get; set; }
        public virtual int OvertimeRate { get; set; }
    }
}
