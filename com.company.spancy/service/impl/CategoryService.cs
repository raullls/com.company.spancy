using com.company.spancy.dto;
using com.company.spancy.entity;
using Spring.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service.impl
{
    public class CategoryService : BaseService<CategoryDto, Category>, ICategoryService
    {
        public object CreateCategoryTX([Validated("categoryValidator")] CategoryDto dto)
        {
            return this.CreateTX(dto);
        }

        public object RemoveCategoryByIdTX(long id)
        {
            IList<Category> result = this.Dao.FindByValueObject("from Category c where c.ParentCategory.Id = :Id", new Category { Id = id });
            foreach (Category category in result)
            {
                category.ParentCategory = null;
                this.Dao.UpdateEntity(category);
            }
            return this.RemoveByIdTX(id);
        }

        public object UpdateCategoryTX([Validated("categoryValidator")] CategoryDto dto)
        {
            return this.UpdateTX(dto);
        }
    }
}
