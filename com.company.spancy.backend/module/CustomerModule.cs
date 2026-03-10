using com.company.spancy.backend.dto;
using com.company.spancy.backend.service;
using Nancy;
using Nancy.ModelBinding;

namespace com.company.spancy.backend.module
{
    public class CustomerModule : NancyModule
    {
        public ICustomerService CustomerService { get; set; }

        public CustomerModule() : base("/api/onetoonebidirectional")
        {
            this.Get("/findAll", x =>
            {
                return this.CustomerService.FindAllRO();
            });

            this.Get("/findById/{customerid}", x =>
            {
                return this.CustomerService.FindByIdRO(x.customerid);
            });

            this.Post("/create", x =>
            {
                CustomerDto customerDto = this.Bind<CustomerDto>();
                Response response = this.CustomerService.CreateCustomerTX(customerDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{customerid}", x =>
            {
                return this.CustomerService.RemoveByIdTX((long)x.customerid);
            });

            this.Post("/edit", x =>
            {
                CustomerDto customerDto = this.Bind<CustomerDto>();
                Response response = this.CustomerService.UpdateCustomerTX(customerDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
