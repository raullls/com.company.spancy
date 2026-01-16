using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class Product
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public override string ToString()
        {
            return $"[Id={Id}, Name={Name}]";
        }
    }
}
