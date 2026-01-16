using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class ManuscriptAuthor
    {
        public virtual Author Author { get; set; }
        public virtual Manuscript Manuscript { get; set; }
        public virtual string Publisher { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            ManuscriptAuthor other = obj as ManuscriptAuthor;

            if (other == null)
                return false;

            return this.Author?.Id == other.Author?.Id && this.Manuscript?.Id == other.Manuscript?.Id;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (Author?.Id.GetHashCode() ?? 0);
                hash = hash * 23 + (Manuscript?.Id.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}