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
    public class CategoryModule : NancyModule
    {
        public ICategoryService CategoryService { get; set; }
        public CategoryModule() : base("/onetomanyselfreference")
        {
            this.Get("/findAll", x =>
            {
                return this.CategoryService.FindAllRO();
            });

            this.Get("/findById/{categoryid}", x =>
            {
                return this.CategoryService.FindByIdRO((long)x.categoryid);
            });

            this.Post("/create", x =>
            {
                CategoryDto categoryDto = this.Bind<CategoryDto>();
                Response response = this.CategoryService.CreateCategoryTX(categoryDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{categoryid}", x =>
            {
                return this.CategoryService.RemoveCategoryByIdTX((long)x.categoryid);
            });

            this.Post("/edit", x =>
            {
                CategoryDto categoryDto = this.Bind<CategoryDto>();
                Response response = this.CategoryService.UpdateCategoryTX(categoryDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
