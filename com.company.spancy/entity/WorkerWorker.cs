using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class WorkerWorker
    {
        public virtual Worker Worker1 { get; set; }
        public virtual Worker Worker2 { get; set; }
        public virtual string Relationship { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            WorkerWorker other = obj as WorkerWorker;

            if (other == null)
                return false;

            return this.Worker1?.Id == other.Worker1?.Id && this.Worker2?.Id == other.Worker2?.Id;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (Worker1?.Id.GetHashCode() ?? 0);
                hash = hash * 23 + (Worker2?.Id.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
