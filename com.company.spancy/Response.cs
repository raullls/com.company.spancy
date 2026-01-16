using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy
{
    public class Response
    {
        public object Data { get; set; }
        public IList<string> Errors { get; set; }
        public Response(object data = null, string uniqueId = null, IList<string> errors = null)
        {
            this.Data = data;
            if (errors != null && !string.IsNullOrEmpty(uniqueId))
            {
                this.Errors = errors.Select(e => $"{uniqueId}: {e}").ToList();
            }
            else
            {
                this.Errors = errors;
            }
        }
    }
}
