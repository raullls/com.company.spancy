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
    public class EmployeeModuleTests : BaseTest
    {
        public IEmployeeService EmployeeService { get; set; }

        [TestMethod]
        public void CreateTest()
        {
            EmployeeDto employeeDto = new PermanentEmployeeDto();
            employeeDto.Name = "ABC";
            employeeDto.Type = "permanentEmployee";
            ((PermanentEmployeeDto)employeeDto).Leaves = 20;
            ((PermanentEmployeeDto)employeeDto).Salary = 1000000;

            Task<BrowserResponse> task = this.Browser.Post("/classtableinheritance/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(employeeDto);
                with.Accept(new Nancy.Responses.Negotiation.MediaRange("application/json"));
            });
            BrowserResponse response = task.Result;

            Assert.AreEqual(Nancy.HttpStatusCode.OK, response.StatusCode);

            string json = response.Body.AsString();

            Response result = JsonConvert.DeserializeObject<Response>(json);
            Assert.IsNull(result.Errors);
        }

        [TestMethod]
        public void UpdateTest()
        {
            this.CreateTest();

            IList<EmployeeDto> employeeDtoList = (this.EmployeeService.FindAllRO() as Response).Data as IList<EmployeeDto>;

            EmployeeDto employeeDto2 = employeeDtoList[0];
            EmployeeDto newEmployeeDto = new PermanentEmployeeDto();
            newEmployeeDto.Id = employeeDto2.Id;
            newEmployeeDto.Name = "DEF";
            newEmployeeDto.Type = "permanentEmployee";
            ((PermanentEmployeeDto)newEmployeeDto).Leaves = 20;
            ((PermanentEmployeeDto)newEmployeeDto).Salary = 1000000;

            Task<BrowserResponse> task = this.Browser.Post("/classtableinheritance/edit", with =>
            {
                with.HttpRequest();
                with.JsonBody(newEmployeeDto);
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

            IList<EmployeeDto> employeeDtoList = (this.EmployeeService.FindAllRO() as Response).Data as IList<EmployeeDto>;

            EmployeeDto employeeDto2 = employeeDtoList[0];

            Task<BrowserResponse> task = this.Browser.Post($"/classtableinheritance/remove/{employeeDto2.Id}", with =>
            {
                with.HttpRequest();
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
