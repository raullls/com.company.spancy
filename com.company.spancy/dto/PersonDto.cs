using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dto
{
    public class PersonDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<string> Numbers { get; set; }
        public override string ToString()
        {
            return $"[Id={Id}, Name={Name}, Numbers={Numbers}]";
        }
    }
}
