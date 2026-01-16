using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dto
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        public override string ToString()
        {
            return $"[Id={Id}, Name={Name}, ParentId={ParentId}]";
        }
    }
}
