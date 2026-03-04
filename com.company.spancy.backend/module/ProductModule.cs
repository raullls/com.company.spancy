using com.company.spancy.backend.dto;
using com.company.spancy.backend.service;
using Nancy;
using Nancy.ModelBinding;

namespace com.company.spancy.backend.module
{
    public class ProductModule : NancyModule
    {
        public IProductService ProductService { get; set; }
        public ProductModule() : base("/api/standalone")
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
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}