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
    public class AccountModule : NancyModule
    {
        public IAccountService AccountService { get; set; }
        public AccountModule() : base("/manytomanybidirectional/account")
        {
            this.Get("/findAll", x =>
            {
                return this.AccountService.FindAllRO();
            });

            this.Get("/findById/{accountid}", x =>
            {
                return this.AccountService.FindByIdRO((long)x.accountid);
            });

            this.Post("/create", x =>
            {
                AccountDto accountDto = this.Bind<AccountDto>();
                Response response = this.AccountService.CreateAccountTX(accountDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{accountid}", x =>
            {
                return this.AccountService.RemoveByIdTX((long)x.accountid);
            });

            this.Post("/edit", x =>
            {
                AccountDto accountDto = this.Bind<AccountDto>();
                Response response = this.AccountService.UpdateAccountTX(accountDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
