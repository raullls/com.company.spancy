using com.company.spancy.backend.dto;

namespace com.company.spancy.backend.service
{
    public interface IProductService : IBaseService<ProductDto>
    {
        object CreateProductTX(ProductDto dto);
        object UpdateProductTX(ProductDto dto);
    }
}