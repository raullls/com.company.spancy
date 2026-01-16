using Spring.Aspects.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.logging
{
    public class CustomLoggingAdvice : SimpleLoggingAdvice
    {
        public string UniqueIdentifier { get; set; }

        protected override string CreateUniqueIdentifier()
        {
            this.UniqueIdentifier = base.CreateUniqueIdentifier();
            return this.UniqueIdentifier;
        }
    }
}
