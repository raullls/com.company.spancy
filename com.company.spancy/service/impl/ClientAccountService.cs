using com.company.spancy.dao;
using com.company.spancy.dto;
using com.company.spancy.entity;
using com.company.spancy.mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service.impl
{
    public class ClientAccountService : IClientAccountService
    {
        public IClientDao ClientDao { get; set; }
        public IAccountDao AccountDao { get; set; }
        public IClientAccountDao ClientAccountDao { get; set; }
        public IBaseMapper<ClientDto, Client> ClientMapper { get; set; }
        public IBaseMapper<AccountDto, Account> AccountMapper { get; set; }
        public object CreateTX(ClientAccountDto dto)
        {
            Client client = this.ClientDao.LoadEntity(dto.ClientDto.Id);
            Account account = this.AccountDao.LoadEntity(dto.AccountDto.Id);
            
            if (client.Accounts == null)
                client.Accounts = new List<Account>();

            client.Accounts.Add(account);
            return this.ClientDao.SaveEntity(client);
        }

        public IList<ClientAccountDto> FindAllRO()
        {
            IList<ClientAccountDto> clientAccountDtos = new List<ClientAccountDto>();
            IList<Client> clientList = this.ClientAccountDao.GetAll();
            foreach (Client client in clientList)
            {
                ClientDto clientDto = this.ClientMapper.Map(client);
                IList<Account> accounts = client.Accounts;
                foreach (Account account in accounts)
                {
                    ClientAccountDto clientAccountDto = new ClientAccountDto();
                    clientAccountDto.ClientDto = clientDto;
                    AccountDto accountDto = this.AccountMapper.Map(account);
                    clientAccountDto.AccountDto = accountDto;
                    clientAccountDtos.Add(clientAccountDto);
                }
            }
            return clientAccountDtos;
        }
        
        public object RemoveTX(ClientAccountDto clientAccountDto)
        {
            Client client = this.ClientDao.LoadEntity(clientAccountDto.ClientDto.Id);
            Account account = this.AccountDao.LoadEntity(clientAccountDto.AccountDto.Id);
            client.Accounts.Remove(account);
            this.ClientDao.UpdateEntity(client);
            return 0;
        }

        public object IsPresentRO(ClientAccountDto clientAccountDto)
        {
            bool status = false;
            IList<Client> clientList = this.ClientAccountDao.IsPresent(clientAccountDto.ClientDto.Id, clientAccountDto.AccountDto.Id);
            if (null != clientList)
            {
                if (clientList.Count() > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public ClientAccountDto FindByIdRO(long id)
        {
            throw new NotImplementedException();
        }

        public object RemoveByIdTX(long id)
        {
            throw new NotImplementedException();
        }

        public object UpdateTX(ClientAccountDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
