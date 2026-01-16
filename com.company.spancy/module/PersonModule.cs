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
    public class PersonModule : NancyModule
    {
        public IPersonService PersonService { get; set; }

        public PersonModule() : base("/onetomanyunidirectional")
        {
            this.Get("/findAll", x =>
            {
                return this.PersonService.FindAllRO();
            });

            this.Get("/findById/{personid}", x =>
            {
                return this.PersonService.FindByIdRO((long)x.personid);
            });

            this.Post("/create", x =>
            {
                PersonDto personDto = this.Bind<PersonDto>();
                Response response = this.PersonService.CreatePersonTX(personDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{personid}", x =>
            {
                return this.PersonService.RemoveByIdTX((long)x.personid);
            });

            this.Post("/edit", x =>
            {
                PersonDto personDto = this.Bind<PersonDto>();
                Response response = this.PersonService.UpdatePersonTX(personDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
