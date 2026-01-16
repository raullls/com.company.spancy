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
    public class UserGroupServiceTests : BaseTest
    {
        public IUserGroupService UserGroupService { get; set; }
        public IUserService UserService { get; set; }
        public IGroupService GroupService { get; set; }
        public UserGroupServiceTests() : base(true) { }

        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0, this.UserGroupService.FindAllRO().Count());
        }

        [TestMethod]
        public void CreateTest()
        {
            UserGroupDto userGroupDto = new UserGroupDto();
            UserDto userDto = new UserDto();
            userDto.Name = "Sara Tencradi";
            this.UserService.CreateTX(userDto);

            GroupDto groupDto = new GroupDto();
            groupDto.Name = "Prison Break";
            this.GroupService.CreateTX(groupDto);

            IList<UserDto> userDtos = this.UserService.FindAllRO();
            UserDto userDto1 = userDtos[0];
            IList<GroupDto> groupDtos = this.GroupService.FindAllRO();
            GroupDto groupDto1 = groupDtos[0];
            userGroupDto.UserDto = userDto1;
            userGroupDto.GroupDto = groupDto1;
            this.UserGroupService.CreateTX(userGroupDto);

            Assert.AreEqual(1, this.UserGroupService.FindAllRO().Count());
        }

        [TestMethod]
        public void RemoveTest()
        {
            UserGroupDto userGroupDto = new UserGroupDto();
            UserDto userDto = new UserDto();
            userDto.Name = "Frodo Baggins";
            this.UserService.CreateTX(userDto);
            GroupDto groupDto = new GroupDto();
            groupDto.Name = "The Lord of the Rings";
            this.GroupService.CreateTX(groupDto);
            
            IList<UserDto> userDtos = this.UserService.FindAllRO();
            UserDto userDto1 = userDtos[0];
            IList<GroupDto> groupDtos = this.GroupService.FindAllRO();
            GroupDto groupDto1 = groupDtos[0];
            userGroupDto.UserDto = userDto1;
            userGroupDto.GroupDto = groupDto1;
            this.UserGroupService.CreateTX(userGroupDto);
            Assert.AreEqual(1, this.UserGroupService.FindAllRO().Count());

            IList<UserGroupDto> uList = this.UserGroupService.FindAllRO();
            UserGroupDto userGroupDto1 = uList[0];
            this.UserGroupService.RemoveTX(userGroupDto1);
            Assert.AreEqual(0, this.UserGroupService.FindAllRO().Count());
        }

        [TestMethod]
        public void IsPresentTest()
        {
            UserGroupDto userGroupDto = new UserGroupDto();
            UserDto userDto = new UserDto();
            userDto.Name = "Frodo Baggins";
            this.UserService.CreateTX(userDto);
            GroupDto groupDto = new GroupDto();
            groupDto.Name = "The Lord of the Rings";
            this.GroupService.CreateTX(groupDto);
            IList<UserDto> userDtos = this.UserService.FindAllRO();
            UserDto userDto1 = userDtos[0];
            IList<GroupDto> groupDtos = this.GroupService.FindAllRO();
            GroupDto groupDto1 = groupDtos[0];
            userGroupDto.UserDto = userDto1;
            userGroupDto.GroupDto = groupDto1;
            this.UserGroupService.CreateTX(userGroupDto);
            Assert.AreEqual(1, this.UserGroupService.FindAllRO().Count());

            bool status = (bool)this.UserGroupService.IsPresentRO(userGroupDto);
            Assert.IsTrue(status);
        }
    }
}
