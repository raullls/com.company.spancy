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
    public class AuthorModule : NancyModule
    {
        public IAuthorService AuthorService { get; set; }
        public AuthorModule() : base("/manytomanybidirectionalwithjoinattribute/author")
        {
            this.Get("/findAll", x =>
            {
                return this.AuthorService.FindAllRO();
            });

            this.Get("/findById/{authorid}", x =>
            {
                return this.AuthorService.FindByIdRO((long)x.authorid);
            });

            this.Post("/create", x =>
            {
                AuthorDto authorDto = this.Bind<AuthorDto>();
                Response response = this.AuthorService.CreateTX(authorDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{authorid}", x =>
            {
                return this.AuthorService.RemoveByIdTX((long)x.authorid);
            });

            this.Post("/edit", x =>
            {
                AuthorDto authorDto = this.Bind<AuthorDto>();
                Response response = this.AuthorService.UpdateTX(authorDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
