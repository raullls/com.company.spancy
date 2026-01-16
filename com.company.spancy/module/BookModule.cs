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

namespace com.company.spancy.module
{
    public class BookModule : NancyModule
    {
        public IBookService BookService { get; set; }
        public BookModule() : base("/onetooneunidirectional")
        {
            this.Get("/findAll", x =>
            {
                return this.BookService.FindAllRO();
            });

            this.Get("/findById/{bookid}", x =>
            {
                return this.BookService.FindByIdRO(x.bookid);
            });

            this.Post("/create", x =>
            {
                BookDto bookDto = this.Bind<BookDto>();
                Response response = this.BookService.CreateBookTX(bookDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });

            this.Post("/remove/{bookid}", x =>
            {
                return this.BookService.RemoveByIdTX((long)x.bookid);
            });

            this.Post("/edit", x =>
            {
                BookDto bookDto = this.Bind<BookDto>();
                Response response = this.BookService.UpdateBookTX(bookDto) as dynamic;
                return Response.AsJson<Response>(response, response.Errors != null && response.Errors.Count > 0 ? HttpStatusCode.Conflict : HttpStatusCode.OK);
            });
        }
    }
}
