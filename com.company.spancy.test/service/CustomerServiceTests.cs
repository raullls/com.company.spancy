using com.company.spancy.backend.dto;
using com.company.spancy.backend.service;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class CustomerServiceTests : BaseTest
    {
        public ICustomerService CustomerService { get; set; } = null!;

        public CustomerServiceTests() : base(true) {}

        [TestMethod]
        public void FindAllTest()
        {
            Assert.IsTrue(this.CustomerService.FindAllRO().Count > 0);
        }

        [TestMethod]
        public void CreateCustomerTest()
        {
            CustomerDto dto = new CustomerDto
            {
                Name = "Alice",
                Amount = 100.50
            };
            long id = (long)this.CustomerService.CreateCustomerTX(dto);

            CustomerDto loaded = this.CustomerService.FindByIdRO(id);
            Assert.AreEqual("Alice", loaded.Name);
            Assert.AreEqual(100.50, loaded.Amount);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            CustomerDto dto = new CustomerDto
            {
                Name = "Bob",
                Amount = 250.00
            };
            long id = (long)this.CustomerService.CreateTX(dto);

            CustomerDto loaded = this.CustomerService.FindByIdRO(id);
            Assert.AreEqual("Bob", loaded.Name);
            Assert.AreEqual(250.00, loaded.Amount);
        }

        [TestMethod]
        public void UpdateCustomerTXTest()
        {
            CustomerDto dto = new CustomerDto
            {
                Name = "Carol",
                Amount = 400.00
            };
            long id = (long)this.CustomerService.CreateCustomerTX(dto);

            CustomerDto toUpdate = this.CustomerService.FindByIdRO(id);
            toUpdate.Name = "Carol Updated";
            toUpdate.Amount = 450.75;

            this.CustomerService.UpdateCustomerTX(toUpdate);

            CustomerDto updated = this.CustomerService.FindByIdRO(id);
            Assert.AreEqual("Carol Updated", updated.Name);
            Assert.AreEqual(450.75, updated.Amount);
        }

        [TestMethod]
        public void RemoveByIdTXTest()
        {
            CustomerDto dto = new CustomerDto
            {
                Name = "Dave",
                Amount = 600.00
            };
            long id = (long)this.CustomerService.CreateTX(dto);

            this.CustomerService.RemoveByIdTX(id);
            IList<CustomerDto> remaining = this.CustomerService.FindAllRO();
            Assert.IsTrue(remaining.All(customer => customer.Id != id));
        }
    }
}
