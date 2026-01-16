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
    public class WorkerModule : NancyModule
    {
        public IWorkerService WorkerService { get; set; }
        public WorkerModule() : base("/manytomanyselfreferencewithjoinattribute/worker")
        {
            this.Get("/findAll", x =>
            {
                return this.WorkerService.FindAllRO();
            });

            this.Get("/findById/{workerid}", x =>
            {
                return this.WorkerService.FindByIdRO((long)x.workerid);
            });

            this.Post("/create", x =>
            {
                WorkerDto workerDto = this.Bind<WorkerDto>();
                Response response = this.WorkerService.CreateTX(workerDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{workerid}", x =>
            {
                return this.WorkerService.RemoveByIdTX((long)x.workerid);
            });

            this.Post("/edit", x =>
            {
                WorkerDto workerDto = this.Bind<WorkerDto>();
                Response response = this.WorkerService.UpdateTX(workerDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
