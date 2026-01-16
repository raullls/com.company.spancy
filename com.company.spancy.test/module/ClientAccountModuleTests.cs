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
    public class ClientAccountModuleTests : BaseTest
    {
        public IClientService ClientService { get; set; }
        public IAccountService AccountService { get; set; }
        
        [TestMethod]
        public void CreateTest()
        {
            ClientAccountDto clientAccountDto = new ClientAccountDto();

            ClientDto clientDto = new ClientDto();
            clientDto.Name = "Sara Tencradi";
            this.ClientService.CreateTX(clientDto);
            
            AccountDto accountDto = new AccountDto();
            accountDto.Number = "Savings Account";
            this.AccountService.CreateTX(accountDto);

            IList<ClientDto> clientDtos = (this.ClientService.FindAllRO() as Response).Data as IList<ClientDto>;
            ClientDto clientDto2 = clientDtos[0];
            IList<AccountDto> accountDtos = (this.AccountService.FindAllRO() as Response).Data as IList<AccountDto>;
            AccountDto accountDto2 = accountDtos[0];
            clientAccountDto.ClientDto = clientDto2;
            clientAccountDto.AccountDto = accountDto2;

            Task<BrowserResponse> task = this.Browser.Post("/manytomanybidirectional/clientaccount/create", with =>
            {
                with.HttpRequest();
                with.JsonBody(clientAccountDto);
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
            ClientAccountDto clientAccountDto = new ClientAccountDto();
            
            IList<ClientDto> clientDtos = (this.ClientService.FindAllRO() as Response).Data as IList<ClientDto>;
            ClientDto clientDto = clientDtos[0];
            IList<AccountDto> accountDtos = (this.AccountService.FindAllRO() as Response).Data as IList<AccountDto>;
            AccountDto accountDto = accountDtos[0];
            
            clientAccountDto.ClientDto = clientDto;
            clientAccountDto.AccountDto = accountDto;

            Task<BrowserResponse> task = this.Browser.Post("/manytomanybidirectional/clientaccount/isPresent", with =>
            {
                with.HttpRequest();
                with.JsonBody(clientAccountDto);
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
            ClientAccountDto clientAccountDto = new ClientAccountDto();

            IList<ClientDto> clientDtos = (this.ClientService.FindAllRO() as Response).Data as IList<ClientDto>;
            ClientDto clientDto = clientDtos[0];
            IList<AccountDto> accountDtos = (this.AccountService.FindAllRO() as Response).Data as IList<AccountDto>;
            AccountDto accountDto = accountDtos[0];
            
            clientAccountDto.ClientDto = clientDto;
            clientAccountDto.AccountDto = accountDto;

            Task<BrowserResponse> task = this.Browser.Post("/manytomanybidirectional/clientaccount/remove", with =>
            {
                with.HttpRequest();
                with.JsonBody(clientAccountDto);
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
