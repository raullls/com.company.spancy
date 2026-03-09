using com.company.spancy.backend.dto;

namespace com.company.spancy.backend.service
{
    public interface IBookService : IBaseService<BookDto>
    {
        object CreateBookTX(BookDto dto);
        object UpdateBookTX(BookDto dto);
    }
}