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
    public class MemberModule : NancyModule
    {
        public IMemberService MemberService { get; set; }
        public MemberModule() : base("/manytomanyselfreference/member")
        {
            this.Get("/findAll", x =>
            {
                return this.MemberService.FindAllRO();
            });

            this.Get("/findById/{memberid}", x =>
            {
                return this.MemberService.FindByIdRO((long)x.memberid);
            });

            this.Post("/create", x =>
            {
                MemberDto memberDto = this.Bind<MemberDto>();
                Response response = this.MemberService.CreateTX(memberDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{memberid}", x =>
            {
                return this.MemberService.RemoveMemberByIdTX((long)x.memberid);
            });

            this.Post("/edit", x =>
            {
                MemberDto memberDto = this.Bind<MemberDto>();
                Response response = this.MemberService.UpdateTX(memberDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
