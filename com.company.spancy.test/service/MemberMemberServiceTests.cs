using com.company.spancy.dto;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class MemberMemberServiceTests : BaseTest
    {
        public IMemberService MemberService { get; set; }
        public IMemberMemberService MemberMemberService { get; set; }
        public MemberMemberServiceTests() : base(true) { }

        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0, this.MemberMemberService.FindAllRO().Count);
        }

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

            IList<MemberDto> memberDtos1 = this.MemberService.FindAllRO();
            MemberDto memberDto3 = memberDtos1[0];

            IList<MemberDto> memberDtos2 = this.MemberService.FindAllRO();
            MemberDto memberDto4 = memberDtos2[0];

            memberMemberDto.MemberId1 = memberDto3;
            memberMemberDto.MemberId2 = memberDto4;

            this.MemberMemberService.CreateTX(memberMemberDto);
            Assert.AreEqual(1, this.MemberMemberService.FindAllRO().Count);
        }

        [TestMethod]
        public void RemoveTest()
        {
            MemberMemberDto memberMemberDto = new MemberMemberDto();

            MemberDto memberDto1 = new MemberDto();
            memberDto1.Name = "Sara Tencradi";
            this.MemberService.CreateTX(memberDto1);

            MemberDto memberDto2 = new MemberDto();
            memberDto2.Name = "Mike Scofield";
            this.MemberService.CreateTX(memberDto2);

            IList<MemberDto> memberDtos1 = this.MemberService.FindAllRO();
            MemberDto memberDto3 = memberDtos1[0];

            IList<MemberDto> memberDtos2 = this.MemberService.FindAllRO();
            MemberDto memberDto4 = memberDtos2[0];

            memberMemberDto.MemberId1 = memberDto3;
            memberMemberDto.MemberId2 = memberDto4;

            this.MemberMemberService.CreateTX(memberMemberDto);
            Assert.AreEqual(1, this.MemberMemberService.FindAllRO().Count);

            IList<MemberMemberDto> memberMemberList = this.MemberMemberService.FindAllRO();
            MemberMemberDto memberMemberDto1 = memberMemberList[0];
            this.MemberMemberService.RemoveTX(memberMemberDto1);

            Assert.AreEqual(0, this.MemberMemberService.FindAllRO().Count);
        }

        [TestMethod]
        public void IsPresentTest()
        {
            MemberMemberDto memberMemberDto = new MemberMemberDto();

            MemberDto memberDto1 = new MemberDto();
            memberDto1.Name = "Sara Tencradi";
            this.MemberService.CreateTX(memberDto1);

            MemberDto memberDto2 = new MemberDto();
            memberDto2.Name = "Mike Scofield";
            this.MemberService.CreateTX(memberDto2);

            IList<MemberDto> memberDtos1 = this.MemberService.FindAllRO();
            MemberDto memberDto3 = memberDtos1[0];

            IList<MemberDto> memberDtos2 = this.MemberService.FindAllRO();
            MemberDto memberDto4 = memberDtos2[0];

            memberMemberDto.MemberId1 = memberDto3;
            memberMemberDto.MemberId2 = memberDto4;

            this.MemberMemberService.CreateTX(memberMemberDto);
            Assert.AreEqual(1, this.MemberMemberService.FindAllRO().Count);

            bool status = (bool)this.MemberMemberService.IsPresentRO(memberMemberDto);
            Assert.IsTrue(status);
        }
    }
}
