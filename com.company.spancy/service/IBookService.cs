using com.company.spancy.dto;
using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service
{
    public interface IBookService : IBaseService<BookDto>
    {
        object CreateBookTX(BookDto dto);
        object UpdateBookTX(BookDto dto);
    }
}
