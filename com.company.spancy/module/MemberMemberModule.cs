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
    public class MemberMemberModule : NancyModule
    {
        public IMemberMemberService MemberMemberService { get; set; }

        public MemberMemberModule() : base("/manytomanyselfreference/membermember")
        {
            this.Get("/findAll", x =>
            {
                return this.MemberMemberService.FindAllRO();
            });

            this.Post("/create", x =>
            {
                MemberMemberDto memberMemberDto = this.Bind<MemberMemberDto>();
                return this.MemberMemberService.CreateTX(memberMemberDto);
            });

            this.Post("/isPresent", x =>
            {
                MemberMemberDto memberMemberDto = this.Bind<MemberMemberDto>();
                return this.MemberMemberService.IsPresentRO(memberMemberDto);
            });

            this.Post("/remove", x =>
            {
                MemberMemberDto memberMemberDto = this.Bind<MemberMemberDto>();
                return this.MemberMemberService.RemoveTX(memberMemberDto);
            });
        }
    }
}
