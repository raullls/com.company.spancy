using com.company.spancy.dto;
using com.company.spancy.service;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.module
{
    public class WorkerWorkerModule : NancyModule
    {
        public IWorkerWorkerService WorkerWorkerService { get; set; }
        
        public WorkerWorkerModule() : base("/manytomanyselfreferencewithjoinattribute/workerworker")
        {
            this.Get("/findAll", x =>
            {
                return this.WorkerWorkerService.FindAllRO();
            });

            this.Post("/create", x =>
            {
                WorkerWorkerDto workerWorkerDto = this.Bind<WorkerWorkerDto>();
                return this.WorkerWorkerService.CreateTX(workerWorkerDto);
            });

            this.Post("/isPresent", x =>
            {
                WorkerWorkerDto workerWorkerDto = this.Bind<WorkerWorkerDto>();
                return this.WorkerWorkerService.IsPresentRO(workerWorkerDto);
            });

            this.Post("/remove", x =>
            {
                WorkerWorkerDto workerWorkerDto = this.Bind<WorkerWorkerDto>();
                return this.WorkerWorkerService.RemoveTX(workerWorkerDto);
            });

        }
    }
}
