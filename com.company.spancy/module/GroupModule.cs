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
    public class GroupModule : NancyModule
    {
        public IGroupService GroupService { get; set; }
        public GroupModule() : base("/manytomanyunidirectional/group")
        {
            this.Get("/findAll", x =>
            {
                return this.GroupService.FindAllRO();
            });

            this.Get("/findById/{groupid}", x =>
            {
                return this.GroupService.FindByIdRO((long)x.groupid);
            });

            this.Post("/create", x =>
            {
                GroupDto groupDto = this.Bind<GroupDto>();
                Response response = this.GroupService.CreateTX(groupDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{groupid}", x =>
            {
                return this.GroupService.RemoveGroupByIdTX((long)x.groupid);
            });

            this.Post("/edit", x =>
            {
                GroupDto groupDto = this.Bind<GroupDto>();
                Response response = this.GroupService.UpdateTX(groupDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
