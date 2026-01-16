using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dto
{
    public class PermanentEmployeeDto : EmployeeDto
    {
        public int Leaves { get; set; }
        public int Salary { get; set; }
    }
}
