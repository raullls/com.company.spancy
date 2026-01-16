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
    public class ClientAccountServiceTests : BaseTest
    {
        public IClientService ClientService { get; set; }
        public IAccountService AccountService { get; set; }
        public IClientAccountService ClientAccountService { get; set; }
        public ClientAccountServiceTests() : base(true) { }

        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0, this.ClientAccountService.FindAllRO().Count());
        }

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
            
            IList<ClientDto> clientDtos = this.ClientService.FindAllRO();
            ClientDto clientDto1 = clientDtos[0];
            
            IList<AccountDto> accountDtos = this.AccountService.FindAllRO();
            AccountDto accountDto1 = accountDtos[0];
            
            clientAccountDto.ClientDto = clientDto1;
            clientAccountDto.AccountDto = accountDto1;
            this.ClientAccountService.CreateTX(clientAccountDto);
            
            Assert.AreEqual(1, this.ClientAccountService.FindAllRO().Count());
        }

        [TestMethod]
        public void RemoveTest()
        {
            ClientAccountDto clientAccountDto = new ClientAccountDto();
            
            ClientDto clientDto = new ClientDto();
            clientDto.Name = "Sara Tencradi";
            this.ClientService.CreateTX(clientDto);

            AccountDto accountDto = new AccountDto();
            accountDto.Number = "Savings Account";
            this.AccountService.CreateTX(accountDto);

            IList<ClientDto> clientDtos = this.ClientService.FindAllRO();
            ClientDto clientDto1 = clientDtos[0];

            IList<AccountDto> accountDtos = this.AccountService.FindAllRO();
            AccountDto accountDto1 = accountDtos[0];
            
            clientAccountDto.ClientDto = clientDto1;
            clientAccountDto.AccountDto = accountDto1;
            this.ClientAccountService.CreateTX(clientAccountDto);
            Assert.AreEqual(1, this.ClientAccountService.FindAllRO().Count());
            
            IList<ClientAccountDto> clientAccountList = this.ClientAccountService.FindAllRO();
            ClientAccountDto clientAccountDto1 = clientAccountList[0];
            this.ClientAccountService.RemoveTX(clientAccountDto1);
            Assert.AreEqual(0, this.ClientAccountService.FindAllRO().Count());
        }

        [TestMethod]
        public void IsPresentTest()
        {
            ClientAccountDto clientAccountDto = new ClientAccountDto();
            
            ClientDto clientDto = new ClientDto();
            clientDto.Name = "Sara Tencradi";
            this.ClientService.CreateTX(clientDto);

            AccountDto accountDto = new AccountDto();
            accountDto.Number = "Savings Account";
            this.AccountService.CreateTX(accountDto);

            IList<ClientDto> clientDtos = this.ClientService.FindAllRO();
            ClientDto clientDto1 = clientDtos[0];
            IList<AccountDto> accountDtos = this.AccountService.FindAllRO();
            AccountDto accountDto1 = accountDtos[0];
            
            clientAccountDto.ClientDto = clientDto1;
            clientAccountDto.AccountDto = accountDto1;
            this.ClientAccountService.CreateTX(clientAccountDto);

            Assert.AreEqual(1, this.ClientAccountService.FindAllRO().Count());
            bool status = (bool)this.ClientAccountService.IsPresentRO(clientAccountDto);
            Assert.IsTrue(status);
        }
    }
}
