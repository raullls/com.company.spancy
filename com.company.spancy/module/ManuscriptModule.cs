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
    public class ManuscriptModule : NancyModule
    {
        public IManuscriptService ManuscriptService { get; set; }
        public ManuscriptModule() : base("/manytomanybidirectionalwithjoinattribute/manuscript")
        {
            this.Get("/findAll", x =>
            {
                return this.ManuscriptService.FindAllRO();
            });

            this.Get("/findById/{manuscriptid}", x =>
            {
                return this.ManuscriptService.FindByIdRO((long)x.clientid);
            });

            this.Post("/create", x =>
            {
                ManuscriptDto manuscriptDto = this.Bind<ManuscriptDto>();
                Response response = this.ManuscriptService.CreateTX(manuscriptDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{manuscriptid}", x =>
            {
                return this.ManuscriptService.RemoveByIdTX((long)x.manuscriptid);
            });

            this.Post("/edit", x =>
            {
                ManuscriptDto manuscriptDto = this.Bind<ManuscriptDto>();
                Response response = this.ManuscriptService.UpdateTX(manuscriptDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
