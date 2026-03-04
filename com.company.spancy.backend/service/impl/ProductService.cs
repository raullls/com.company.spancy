using com.company.spancy.backend.dto;
using com.company.spancy.backend.entity;
using Spring.Validation;

namespace com.company.spancy.backend.service.impl
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