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
    public class CustomerModule : NancyModule
    {
        public ICustomerService CustomerService { get; set; }
        public CustomerModule() : base("/onetoonebidirectional")
        {
            this.Get("/findAll", x =>
            {
                return this.CustomerService.FindAllRO();
            });

            this.Get("/findById/{customerid}", x =>
            {
                return this.CustomerService.FindByIdRO((long)x.customerid);
            });

            this.Post("/create", x =>
            {
                CustomerDto customerDto = this.Bind<CustomerDto>();
                Response response = this.CustomerService.CreateTX(customerDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{customerid}", x =>
            {
                return this.CustomerService.RemoveByIdTX((long)x.customerid);
            });

            this.Post("/edit", x =>
            {
                CustomerDto customerDto = this.Bind<CustomerDto>();
                Response response = this.CustomerService.UpdateTX(customerDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
