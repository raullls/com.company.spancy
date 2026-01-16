using com.company.spancy.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service
{
    public interface IWorkerWorkerService : IBaseService<WorkerWorkerDto>
    {
        object RemoveTX(WorkerWorkerDto dto);
        object IsPresentRO(WorkerWorkerDto dto);
    }
}
