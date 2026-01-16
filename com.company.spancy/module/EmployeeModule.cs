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
    public class EmployeeModule : NancyModule
    {
        public IEmployeeService EmployeeService { get; set; }
        public EmployeeModule() : base("/classtableinheritance")
        {
            this.Get("/findAll", x =>
            {
                return this.EmployeeService.FindAllRO();
            });

            this.Get("/findById/{employeeid}", x =>
            {
                return this.EmployeeService.FindByIdRO((long)x.employeeid);
            });

            this.Post("/create", x =>
            {
                EmployeeDto employeeDto = this.Bind<EmployeeDto>();
                Response response = this.EmployeeService.CreateTX(employeeDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/edit", x =>
            {
                EmployeeDto employeeDto = this.Bind<EmployeeDto>();
                Response response = this.EmployeeService.UpdateTX(employeeDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{employeeid}", x =>
            {
                return this.EmployeeService.RemoveByIdTX((long)x.employeeid);
            });
        }
    }
}
