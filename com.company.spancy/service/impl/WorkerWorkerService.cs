using com.company.spancy.dao;
using com.company.spancy.dto;
using com.company.spancy.entity;
using com.company.spancy.mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service.impl
{
    public class WorkerWorkerService : IWorkerWorkerService
    {
        public IWorkerDao WorkerDao { get; set; }
        public IWorkerWorkerDao WorkerWorkerDao { get; set; }
        public IBaseMapper<WorkerDto,Worker> WorkerMapper { get; set; }
        public object CreateTX(WorkerWorkerDto dto)
        {
            Worker worker1 = this.WorkerDao.LoadEntity(dto.WorkerId1.Id);
            Worker worker2 = this.WorkerDao.LoadEntity(dto.WorkerId2.Id);

            WorkerWorker workerWorker = new WorkerWorker();
            workerWorker.Worker1 = worker1;
            workerWorker.Worker2 = worker2;
            workerWorker.Relationship = dto.RelationshipType;

            if (worker1.WorkerWorkers == null) worker1.WorkerWorkers = new List<WorkerWorker>();

            worker1.WorkerWorkers.Add(workerWorker);
            return this.WorkerDao.SaveEntity(worker1);
        }

        public IList<WorkerWorkerDto> FindAllRO()
        {
            IList<WorkerWorkerDto> workerWorkerDtos = new List<WorkerWorkerDto>();
            IList<WorkerWorker> workerList = WorkerWorkerDao.GetAll();
            foreach (WorkerWorker workerWorker in workerList)
            {
                WorkerWorkerDto workerWorkerDto = new WorkerWorkerDto();
                workerWorkerDto.WorkerId1 = WorkerMapper.Map(workerWorker.Worker1);
                workerWorkerDto.WorkerId2 = WorkerMapper.Map(workerWorker.Worker2);
                workerWorkerDto.RelationshipType = workerWorker.Relationship;
                workerWorkerDtos.Add(workerWorkerDto);
            }
            return workerWorkerDtos;
        }

        public WorkerWorkerDto FindByIdRO(long id)
        {
            throw new NotImplementedException();
        }

        public object IsPresentRO(WorkerWorkerDto dto)
        {
            bool status = false;
            IList<WorkerWorker> workerWorkerList = this.WorkerWorkerDao.IsPresent(dto.WorkerId1.Id, dto.WorkerId2.Id);
            if (workerWorkerList.Count() > 0)
            {
                status = true;
            }
            else
            {
                workerWorkerList = this.WorkerWorkerDao.IsPresent(dto.WorkerId2.Id, dto.WorkerId1.Id);
                if (workerWorkerList.Count() > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public object RemoveByIdTX(long id)
        {
            throw new NotImplementedException();
        }

        public object RemoveTX(WorkerWorkerDto dto)
        {
            Worker worker1 = this.WorkerDao.LoadEntity(dto.WorkerId1.Id);
            Worker worker2 = this.WorkerDao.LoadEntity(dto.WorkerId2.Id);

            IList<WorkerWorker> workerWorkerList = this.WorkerWorkerDao.IsPresent(dto.WorkerId1.Id, dto.WorkerId2.Id);
            foreach (WorkerWorker workerWorker in workerWorkerList)
            {
                worker1.WorkerWorkers.Remove(workerWorker);
                //worker2.WorkerWorkers.Remove(workerWorker);
            }
            this.WorkerDao.UpdateEntity(worker1);
            this.WorkerDao.UpdateEntity(worker2);
            return 0;
        }

        public object UpdateTX(WorkerWorkerDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
