using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.dao
{
    public interface IWorkerWorkerDao
    {
        IList<WorkerWorker> GetAll();
        IList<WorkerWorker> IsPresent(long worker1Id, long worker2Id);
    }
}
