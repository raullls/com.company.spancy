using com.company.spancy.dto;
using com.company.spancy.entity;
using com.company.spancy.service;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Responses;

namespace com.company.spancy.module
{
    public class ProductModule : NancyModule
    {
        public IProductService ProductService { get; set; }
        public ProductModule() : base("/standalone")
        {
            this.Get("/findAll", x =>
            {
                return this.ProductService.FindAllRO();
            });

            this.Get("/findById/{productid}", x =>
            {
                return this.ProductService.FindByIdRO(x.productid);
            });

            this.Post("/create", x =>
            {
                ProductDto productDto = this.Bind<ProductDto>();
                Response response = this.ProductService.CreateProductTX(productDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{productid}", x =>
            {
                return this.ProductService.RemoveByIdTX((long)x.productid);
            });

            this.Post("/edit", x =>
            {
                ProductDto productDto = this.Bind<ProductDto>();
                Response response = this.ProductService.UpdateProductTX(productDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
