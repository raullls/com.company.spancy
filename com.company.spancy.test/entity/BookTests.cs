using com.company.spancy.backend.entity;

namespace com.company.spancy.test.entity
{
    [TestClass]
    public class BookTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Book b1 = new Book();
            b1.Name = "A";
            Shipping shipping1 = new Shipping();
            shipping1.City = "US";
            b1.Shipping = shipping1;
            this.SessionFactory.GetCurrentSession().Save(b1);

            Book b2 = new Book();
            b2.Name = "B";
            Shipping shipping2 = new Shipping();
            shipping2.City = "CAN";
            b2.Shipping = shipping2;
            this.SessionFactory.GetCurrentSession().Save(b2);

            b1.Name = "C";
            shipping1 = b1.Shipping;
            shipping1.City = "UK";
            this.SessionFactory.GetCurrentSession().Merge(b1);

            IList<Book> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Book").List<Book>();
            Assert.IsTrue(list.Count > 0);

            this.SessionFactory.GetCurrentSession().Delete(b2);
            list = this.SessionFactory.GetCurrentSession().CreateQuery("from Book").List<Book>();
            Assert.IsTrue(list.Count > 0);
        }
    }
}
