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
    public class UserModule : NancyModule
    {
        public IUserService UserService { get; set; }
        public UserModule() : base("/manytomanyunidirectional/user")
        {
            this.Get("/findAll", x =>
            {
                return this.UserService.FindAllRO();
            });

            this.Get("/findById/{userid}", x =>
            {
                return this.UserService.FindByIdRO((long)x.userid);
            });

            this.Post("/create", x =>
            {
                UserDto userDto = this.Bind<UserDto>();
                Response response = this.UserService.CreateTX(userDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{userid}", x =>
            {
                return this.UserService.RemoveByIdTX((long)x.userid);
            });

            this.Post("/edit", x =>
            {
                UserDto userDto = this.Bind<UserDto>();
                Response response = this.UserService.UpdateTX(userDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
