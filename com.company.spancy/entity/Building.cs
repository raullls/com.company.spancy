using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class Building : Estate
    {
        public virtual int Floors { get; set; }
    }
}
