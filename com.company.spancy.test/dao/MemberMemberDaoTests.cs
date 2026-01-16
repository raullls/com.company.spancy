using com.company.spancy.dao;
using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class MemberMemberDaoTests : BaseTest
    {
        public IMemberDao MemberDao { get; set; }
        public IMemberMemberDao MemberMemberDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.MemberMemberDao.GetAll().Count);
        }

        [TestMethod]
        public void IsPresentTest()
        {
            bool status = false;
            Member m1 = new Member();
            m1.Name = "Alexander Mahone";
            this.MemberDao.SaveEntity(m1);

            Member m2 = new Member();
            m2.Name = "Credit Member";
            this.MemberDao.SaveEntity(m2);

            IList<Member> memberList = this.MemberDao.LoadAllEntities();
            Member member = memberList[0];

            IList<Member> memberList2 = this.MemberDao.LoadAllEntities();
            Member member2 = memberList2[0];

            member.Members.Add(member2);
            this.MemberDao.SaveEntity(member);

            IList<Member> memberList3 = this.MemberMemberDao.IsPresent(member.Id, member2.Id);
            if (null != memberList3)
            {
                if (memberList3.Count > 0)
                {
                    status = true;
                }
            }
            Assert.IsTrue(status);
        }
    }
}
