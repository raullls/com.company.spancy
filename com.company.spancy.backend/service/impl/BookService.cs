using com.company.spancy.backend.dto;
using com.company.spancy.backend.entity;
using Spring.Validation;

namespace com.company.spancy.backend.service.impl
{
    public class BookService : BaseService<BookDto,Book>, IBookService
    {
        public object CreateBookTX([Validated("bookValidator")] BookDto bookDto)
        {
            return base.CreateTX(bookDto);
        }

        public object UpdateBookTX([Validated("bookValidator")] BookDto bookDto)
        {
            Book book = this.Mapper.Map(bookDto);
            book.Shipping.Id = this.Dao.FindByValueObject("from Book book where book.Id = :Id", book).Single().Shipping.Id;
            if (this.Dao.FindByValueObject(this.Hql, book).Count == 0)
            {
                this.Dao.UpdateEntity(book);
                return null;
            }
            else
            {
                throw new Exception($"{MessageSource.GetMessage(ErrorMsges[0], new object[] { bookDto })}");
            }
        }
    }
}