using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class User
    {
        public User()
        {
            this.Groups = new List<Group>();
        }
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Group> Groups { get; set; }
    }
}
