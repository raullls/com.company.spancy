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
    public class ClientModule : NancyModule
    {
        public IClientService ClientService { get; set; }
        public ClientModule() : base("/manytomanybidirectional/client")
        {
            this.Get("/findAll", x =>
            {
                return this.ClientService.FindAllRO();
            });

            this.Get("/findById/{clientid}", x =>
            {
                return this.ClientService.FindByIdRO((long)x.clientid);
            });

            this.Post("/create", x =>
            {
                ClientDto clientDto = this.Bind<ClientDto>();
                Response response = this.ClientService.CreateTX(clientDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{clientid}", x =>
            {
                return this.ClientService.RemoveByIdTX((long)x.clientid);
            });

            this.Post("/edit", x =>
            {
                ClientDto clientDto = this.Bind<ClientDto>();
                Response response = this.ClientService.UpdateTX(clientDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
