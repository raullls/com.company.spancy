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
    public class EstateModule : NancyModule
    {
        public IEstateService EstateService { get; set; }
        public EstateModule() : base("/concretetableinheritance")
        {
            this.Get("/findAll", x =>
            {
                return this.EstateService.FindAllRO();
            });

            this.Get("/findById/{estateid}", x =>
            {
                return this.EstateService.FindByIdRO((long)x.estateid);
            });

            this.Post("/create", x =>
            {
                EstateDto estateDto = this.Bind<EstateDto>();
                Response response = this.EstateService.CreateTX(estateDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/edit", x =>
            {
                EstateDto estateDto = this.Bind<EstateDto>();
                Response response = this.EstateService.UpdateTX(estateDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{estateid}", x =>
            {
                return this.EstateService.RemoveByIdTX((long)x.estateid);
            });
        }
    }
}
