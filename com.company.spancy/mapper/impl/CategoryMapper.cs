using AutoMapper;
using com.company.spancy.dto;
using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.mapper.impl
{
    public class CategoryMapper : BaseMapper<CategoryDto, Category>, IBaseMapper<CategoryDto, Category>
    {
        public CategoryMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryDto, Category>()
                    .ForMember(dest => dest.ParentCategory, opt => opt.MapFrom((src, dest, ctx) =>
                    {
                        Category parentCategory = null;
                        if (src.ParentId > 0)
                        {
                            parentCategory = this.Dao.FindByValueObject(this.Hql, new Category { Id = src.ParentId }).Single();
                            this.Dao.Evict(parentCategory);
                        }
                        return parentCategory;
                    }));
                cfg.CreateMap<Category, CategoryDto>()
                    .ForMember(dest => dest.ParentId, opt => opt.MapFrom((src, dest, ctx) =>
                    {
                        return src.ParentCategory?.Id ?? 0;
                    }));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
