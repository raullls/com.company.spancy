using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class Author
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<ManuscriptAuthor> ManuscriptAuthors { get; set; } = new List<ManuscriptAuthor>();
    }
}
