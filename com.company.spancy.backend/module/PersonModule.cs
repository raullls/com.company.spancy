using com.company.spancy.backend.dto;
using com.company.spancy.backend.service;
using Nancy;
using Nancy.ModelBinding;

namespace com.company.spancy.backend.module
{
    public class PersonModule : NancyModule
    {
        public IPersonService PersonService { get; set; }
        public PersonModule() : base("/api/onetomanyunidirectional")
        {
            this.Get("/findAll", x =>
            {
                return this.PersonService.FindAllRO();
            });

            this.Get("/findById/{pid}", x =>
            {
                return this.PersonService.FindByIdRO((long)x.pid);
            });

            this.Post("/create", x =>
            {
                PersonDto personDto = this.Bind<PersonDto>();
                Response response = this.PersonService.CreatePersonTX(personDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{pid}", x =>
            {
                Response response = this.PersonService.RemovePersonTX((long)x.pid) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
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