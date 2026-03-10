using com.company.spancy.backend.dto;
using com.company.spancy.backend.entity;
using Spring.Validation;

namespace com.company.spancy.backend.service.impl
{
    public class CustomerService : BaseService<CustomerDto, Customer>, ICustomerService
    {
        public object CreateCustomerTX([Validated("customerValidator")] CustomerDto customerDto)
        {
            return base.CreateTX(customerDto);
        }

        public object UpdateCustomerTX([Validated("customerValidator")] CustomerDto customerDto)
        {
            Customer customer = this.Mapper.Map(customerDto);
            customer.Cart.Id = this.Dao.FindByValueObject("from Customer customer where customer.Id = :Id", customer).Single().Cart.Id;
            if (this.Dao.FindByValueObject(this.Hql, customer).Count == 0)
            {
                this.Dao.UpdateEntity(customer);
                return null;
            }
            else
            {
                throw new Exception($"{MessageSource.GetMessage(ErrorMsges[0], new object[] { customerDto })}");
            }
        }
    }
}
