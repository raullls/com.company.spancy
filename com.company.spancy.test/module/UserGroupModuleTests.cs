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
    public class UserGroupModuleTests : BaseTest
    {
        public IUserService UserService { get; set; }
        public IGroupService GroupService { get; set; }

        [TestMethod]
        public void PostCreateTest()
        {
            UserGroupDto userGroupDto = new UserGroupDto();
            
            UserDto userDto = new UserDto();
            userDto.Name = "Sara Tencradi";
            this.UserService.CreateTX(userDto);

            GroupDto groupDto = new GroupDto();
            groupDto.Name = "Prison Break";
            this.GroupService.CreateTX(groupDto);

            IList<UserDto> userDtos = (this.UserService.FindAllRO() as Response).Data as IList<UserDto>;
            UserDto userDto1 = userDtos[0];
            IList<GroupDto> groupDtos = (this.GroupService.FindAllRO() as Response).Data as IList<GroupDto>;
            GroupDto groupDto1 = groupDtos[0];
            userGroupDto.UserDto = userDto1;
            userGroupDto.GroupDto = groupDto1;

            Task<BrowserResponse> task = this.Browser.Post("/manytomanyunidirectional/usergroup/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(userGroupDto);
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
            this.PostCreateTest();
            UserGroupDto userGroupDto = new UserGroupDto();
            IList<UserDto> userDtos = (this.UserService.FindAllRO() as Response).Data as IList<UserDto>;
            UserDto userDto = userDtos[0];

            IList<GroupDto> groupDtos = (this.GroupService.FindAllRO() as Response).Data as IList<GroupDto>;
            GroupDto groupDto = groupDtos[0];
            
            userGroupDto.UserDto = userDto;
            userGroupDto.GroupDto = groupDto;

            Task<BrowserResponse> task = this.Browser.Post("/manytomanyunidirectional/usergroup/isPresent", with =>
            {
                with.HttpRequest();
                with.JsonBody(userGroupDto);
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
            this.PostCreateTest();
            UserGroupDto userGroupDto = new UserGroupDto();
            IList<UserDto> userDtos = (this.UserService.FindAllRO() as Response).Data as IList<UserDto>;
            UserDto userDto = userDtos[0];
            IList<GroupDto> groupDtos = (this.GroupService.FindAllRO() as Response).Data as IList<GroupDto>;
            GroupDto groupDto = groupDtos[0];
            userGroupDto.UserDto = userDto;
            userGroupDto.GroupDto = groupDto;

            Task<BrowserResponse> task = this.Browser.Post("/manytomanyunidirectional/usergroup/remove", with =>
            {
                with.HttpRequest();
                with.JsonBody(userGroupDto);
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
