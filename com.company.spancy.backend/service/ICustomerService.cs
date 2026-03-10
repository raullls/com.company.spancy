using com.company.spancy.backend.dto;

namespace com.company.spancy.backend.service
{
    public interface ICustomerService : IBaseService<CustomerDto>
    {
        object CreateCustomerTX(CustomerDto dto);
        object UpdateCustomerTX(CustomerDto dto);
    }
}
