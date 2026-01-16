using com.company.spancy.dto;
using com.company.spancy.service;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.module
{
    public class ClientAccountModule : NancyModule
    {
        public IClientAccountService ClientAccountService { get; set; }
        public ClientAccountModule() : base("/manytomanybidirectional/clientaccount")
        {
            this.Get("/findAll", x =>
            {
                return this.ClientAccountService.FindAllRO();
            });

            this.Post("/create", x =>
            {
                ClientAccountDto clientAccountDto = this.Bind<ClientAccountDto>();
                return this.ClientAccountService.CreateTX(clientAccountDto);
            });

            this.Post("/isPresent", x =>
            {
                ClientAccountDto clientAccountDto = this.Bind<ClientAccountDto>();
                return this.ClientAccountService.IsPresentRO(clientAccountDto);
            });

            this.Post("/remove", x =>
            {
                ClientAccountDto clientAccountDto = this.Bind<ClientAccountDto>();
                return this.ClientAccountService.RemoveTX(clientAccountDto);
            });
        }
    }
}
