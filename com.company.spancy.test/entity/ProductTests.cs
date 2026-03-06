using com.company.spancy.backend.entity;

namespace com.company.spancy.test.entity
{
    [TestClass]
    public class ProductTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Product p1 = new Product();
            p1.Name = "A";

            Product p2 = new Product();
            p2.Name = "B";

            this.SessionFactory.GetCurrentSession().Save(p1);
            this.SessionFactory.GetCurrentSession().Save(p2);

            p1.Name = "C";
            this.SessionFactory.GetCurrentSession().Merge(p1);

            IList<Product> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Product").List<Product>();
            Assert.IsTrue(list.Count > 0);

            this.SessionFactory.GetCurrentSession().Delete(p1);
            IList<Product> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from Product").List<Product>();
            Assert.IsTrue(list.Count > 0);
        }
    }
}