using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class Member
    {
        public Member()
        {
            this.Members = new List<Member>();
            this.ReverseMembers = new List<Member>();
        }
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Member> Members { get; set; }
        public virtual IList<Member> ReverseMembers { get; set; }
    }
}
