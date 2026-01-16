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
    public class UserGroupModule : NancyModule
    {
        public IUserGroupService UserGroupService { get; set; }
        public UserGroupModule() : base("/manytomanyunidirectional/usergroup")
        {
            this.Get("/findAll", x =>
            {
                return this.UserGroupService.FindAllRO();
            });

            this.Post("/isPresent", x =>
            {
                UserGroupDto userGroupDto = this.Bind<UserGroupDto>();
                return this.UserGroupService.IsPresentRO(userGroupDto);
            });

            this.Post("/create", x =>
            {
                UserGroupDto userGroupDto = this.Bind<UserGroupDto>();
                return this.UserGroupService.CreateTX(userGroupDto);
            });

            this.Post("/remove", x =>
            {
                UserGroupDto userGroupDto = this.Bind<UserGroupDto>();
                Response response = this.UserGroupService.RemoveTX(userGroupDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.NoContent : HttpStatusCode.OK);
            });
        }
    }
}
