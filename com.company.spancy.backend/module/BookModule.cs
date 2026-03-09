using com.company.spancy.backend.dto;
using com.company.spancy.backend.service;
using Nancy;
using Nancy.ModelBinding;

namespace com.company.spancy.backend.module
{
    public class BookModule : NancyModule
    {
        public IBookService BookService { get; set; }
        public BookModule() : base("/api/onetooneunidirectional")
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