using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dto
{
    public class CustomerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public override string ToString()
        {
            return $"[Id={Id}, Name={Name}, Amount={Amount}]";
        }
    }
}
