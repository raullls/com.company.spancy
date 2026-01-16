using com.company.spancy.dao;
using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class UserGroupDaoTests : BaseTest
    {
        public IUserDao UserDao { get; set; }
        public IGroupDao GroupDao { get; set; }
        public IUserGroupDao UserGroupDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.UserGroupDao.GetAll().Count);
        }

        [TestMethod]
        public void IsPresentTest()
        {
            bool status = false;

            User u1 = new User();
            u1.Name = "Alexander Mahone";
            this.UserDao.SaveEntity(u1);

            Group g1 = new Group();
            g1.Name = "Java User Group";
            this.GroupDao.SaveEntity(g1);

            IList<User> userList = this.UserDao.LoadAllEntities();
            User user = userList[0];

            IList<Group> groupList = this.GroupDao.LoadAllEntities();
            Group group = groupList[0];
            user.Groups.Add(group);
            this.UserDao.SaveEntity(user);

            IList<User> userList2 = this.UserGroupDao.IsPresent(user.Id, group.Id);
            if (null != userList2)
            {
                if (userList2.Count > 0)
                {
                    status = true;
                }
            }
            Assert.IsTrue(status);
        }
    }
}
