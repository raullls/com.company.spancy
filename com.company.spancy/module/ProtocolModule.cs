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
    public class ProtocolModule : NancyModule
    {
        public IProtocolService ProtocolService { get; set; }
        public ProtocolModule() : base("/singletableinheritance")
        {
            this.Get("/findAll", x =>
            {
                return this.ProtocolService.FindAllRO();
            });

            this.Get("/findById/{protocolid}", x =>
            {
                return this.ProtocolService.FindByIdRO((long)x.protocolid);
            });

            this.Post("/create", x =>
            {
                ProtocolDto protocolDto = this.Bind<ProtocolDto>();
                Response response = this.ProtocolService.CreateProtocolTX(protocolDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{protocolid}", x =>
            {
                return this.ProtocolService.RemoveByIdTX((long)x.protocolid);
            });

            this.Post("/edit", x =>
            {
                ProtocolDto protocolDto = this.Bind<ProtocolDto>();
                Response response = this.ProtocolService.UpdateProtocolTX(protocolDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
