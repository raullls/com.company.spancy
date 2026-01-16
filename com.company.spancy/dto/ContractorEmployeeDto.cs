using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dto
{
    public class ContractorEmployeeDto : EmployeeDto
    {
        public int HourleyRate { get; set; }
        public int OvertimeRate { get; set; }
    }
}
