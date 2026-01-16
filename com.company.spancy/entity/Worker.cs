using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.entity
{
    public class Worker
    {
        public Worker()
        {
            this.WorkerWorkers = new List<WorkerWorker>();
        }
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<WorkerWorker> WorkerWorkers { get; set; }
    }
}
