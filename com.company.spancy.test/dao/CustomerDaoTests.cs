using com.company.spancy.backend.dao;
using com.company.spancy.backend.entity;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class CustomerDaoTests : BaseTest
    {
        public IBaseDao<Customer> CustomerDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.IsTrue(this.CustomerDao.LoadAllEntities().Count > 0);
        }

        [TestMethod]
        public void InsertTest()
        {
            Customer firstCustomer = new Customer();
            firstCustomer.Name = "Alice";
            Cart firstCart = new Cart();
            firstCart.Amount = 100.50;
            firstCustomer.Cart = firstCart;
            this.CustomerDao.SaveEntity(firstCustomer);

            Customer secondCustomer = new Customer();
            secondCustomer.Name = "Bob";
            Cart secondCart = new Cart();
            secondCart.Amount = 250.00;
            secondCustomer.Cart = secondCart;
            this.CustomerDao.SaveEntity(secondCustomer);

            Assert.IsTrue(this.CustomerDao.LoadAllEntities().Count > 0);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Customer customerToSave = new Customer();
            customerToSave.Name = "Alice";
            Cart cart = new Cart();
            cart.Amount = 100.50;
            customerToSave.Cart = cart;
            long id = this.CustomerDao.SaveEntity(customerToSave);

            Customer loadedCustomer = this.CustomerDao.LoadEntity(id);
            Assert.AreEqual("Alice", loadedCustomer.Name);
            Assert.AreEqual(100.50, loadedCustomer.Cart.Amount);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Customer firstCustomer = new Customer();
            firstCustomer.Name = "Alice";
            Cart firstCart = new Cart();
            firstCart.Amount = 100.50;
            firstCustomer.Cart = firstCart;
            this.CustomerDao.SaveEntity(firstCustomer);

            Customer secondCustomer = new Customer();
            secondCustomer.Name = "Bob";
            Cart secondCart = new Cart();
            secondCart.Amount = 250.00;
            secondCustomer.Cart = secondCart;
            this.CustomerDao.SaveEntity(secondCustomer);

            Assert.IsTrue(this.CustomerDao.LoadAllEntities().Count > 0);

            this.CustomerDao.DeleteEntity(secondCustomer);

            Assert.IsTrue(this.CustomerDao.LoadAllEntities().Count > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Customer customerToSave = new Customer();
            customerToSave.Name = "Alice";
            Cart cart = new Cart();
            cart.Amount = 100.50;
            customerToSave.Cart = cart;
            this.CustomerDao.SaveEntity(customerToSave);

            Assert.IsTrue(this.CustomerDao.LoadAllEntities().Count > 0);

            IList<Customer> customers = this.CustomerDao.LoadAllEntities();
            Customer customer = customers[0];
            customer.Name = "Alice Updated";
            customer.Cart.Amount = 175.25;

            this.CustomerDao.UpdateEntity(customer);

            IList<Customer> updatedCustomers = this.CustomerDao.LoadAllEntities();
            Customer updatedCustomer = updatedCustomers[0];
            Assert.AreEqual("Alice Updated", updatedCustomer.Name);
            Assert.AreEqual(175.25, updatedCustomer.Cart.Amount);
        }
    }
}
