using com.company.spancy.backend.entity;

namespace com.company.spancy.test.entity
{
    [TestClass]
    public class CustomerTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Customer c1 = new Customer();
            c1.Name = "A";
            Cart cart1 = new Cart();
            cart1.Amount = 100.00;
            c1.Cart = cart1;
            this.SessionFactory.GetCurrentSession().Save(c1);

            Customer c2 = new Customer();
            c2.Name = "B";
            Cart cart2 = new Cart();
            cart2.Amount = 200.00;
            c2.Cart = cart2;
            this.SessionFactory.GetCurrentSession().Save(c2);

            c1.Name = "C";
            c1.Cart.Amount = 300.00;
            this.SessionFactory.GetCurrentSession().Merge(c1);

            IList<Customer> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Customer").List<Customer>();
            Assert.IsTrue(list.Count > 0);

            this.SessionFactory.GetCurrentSession().Delete(c2);
            list = this.SessionFactory.GetCurrentSession().CreateQuery("from Customer").List<Customer>();
            Assert.IsTrue(list.Count > 0);
        }
    }
}
