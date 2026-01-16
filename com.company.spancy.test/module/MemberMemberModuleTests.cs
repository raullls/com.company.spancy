using com.company.spancy.dto;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nancy.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.module
{
    [TestClass]
    public class MemberMemberModuleTests : BaseTest
    {
        public IMemberService MemberService { get; set; }

        [TestMethod]
        public void CreateTest()
        {
            MemberMemberDto memberMemberDto = new MemberMemberDto();

            MemberDto memberDto1 = new MemberDto();
            memberDto1.Name = "Sara Tencradi";
            this.MemberService.CreateTX(memberDto1);

            MemberDto memberDto2 = new MemberDto();
            memberDto2.Name = "Mike Scofield";
            this.MemberService.CreateTX(memberDto2);

            IList<MemberDto> memberDtos1 = (this.MemberService.FindAllRO() as Response).Data as IList<MemberDto>;
            MemberDto memberDto3 = memberDtos1[0];

            IList<MemberDto> memberDtos2 = (this.MemberService.FindAllRO() as Response).Data as IList<MemberDto>;
            MemberDto memberDto4 = memberDtos2[0];

            memberMemberDto.MemberId1 = memberDto3;
            memberMemberDto.MemberId2 = memberDto4;
            
            Task<BrowserResponse> task = this.Browser.Post("/manytomanyselfreference/membermember/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(memberMemberDto);
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);
        }

        [TestMethod]
        public void PresentTest()
        {
            this.CreateTest();
            MemberMemberDto memberMemberDto = new MemberMemberDto();

            IList<MemberDto> memberDtos1 = (this.MemberService.FindAllRO() as Response).Data as IList<MemberDto>;
            MemberDto memberDto1 = memberDtos1[0];

            IList<MemberDto> memberDtos2 = (this.MemberService.FindAllRO() as Response).Data as IList<MemberDto>;
            MemberDto memberDto2 = memberDtos2[0];

            memberMemberDto.MemberId1 = memberDto1;
            memberMemberDto.MemberId2 = memberDto2;

            Task<BrowserResponse> task = this.Browser.Post("/manytomanyselfreference/membermember/isPresent", with =>
            {
                with.HttpRequest();
                with.JsonBody(memberMemberDto);
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);
        }

        [TestMethod]
        public void DeleteTest()
        {
            this.CreateTest();
            MemberMemberDto memberMemberDto = new MemberMemberDto();

            IList<MemberDto> memberDtos1 = (this.MemberService.FindAllRO() as Response).Data as IList<MemberDto>;
            MemberDto memberDto1 = memberDtos1[0];

            IList<MemberDto> memberDtos2 = (this.MemberService.FindAllRO() as Response).Data as IList<MemberDto>;
            MemberDto memberDto2 = memberDtos2[0];

            memberMemberDto.MemberId1 = memberDto1;
            memberMemberDto.MemberId2 = memberDto2;

            Task<BrowserResponse> task = this.Browser.Post("/manytomanyselfreference/membermember/remove", with =>
            {
                with.HttpRequest();
                with.JsonBody(memberMemberDto);
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);
        }
    }
}
