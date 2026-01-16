using com.company.spancy.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service
{
    public interface ICategoryService : IBaseService<CategoryDto>
    {
        object UpdateCategoryTX(CategoryDto dto);
        object CreateCategoryTX(CategoryDto dto);
        object RemoveCategoryByIdTX(long id);
    }
}
