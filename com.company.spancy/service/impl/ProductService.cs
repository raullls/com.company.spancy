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
    public class ProductService : BaseService<ProductDto, Product>, IProductService
    {
        public object CreateProductTX([Validated("productValidator")] ProductDto productDto)
        {
            return this.CreateTX(productDto);
        }

        public object UpdateProductTX([Validated("productValidator")] ProductDto productDto)
        {
            return this.UpdateTX(productDto);
        }
    }
}
