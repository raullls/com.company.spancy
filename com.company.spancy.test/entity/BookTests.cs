using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.entity
{
    [TestClass]
    public class BookTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Book book1 = new Book();
            book1.Name = "Java SE";
            Shipping shipping1 = new Shipping();
            shipping1.City = "US";
            book1.Shipping = shipping1;
            this.SessionFactory.GetCurrentSession().Save(book1);

            Book book2 = new Book();
            book2.Name = "EJB 3.0";
            Shipping shipping2 = new Shipping();
            shipping2.City = "CAN";
            book2.Shipping = shipping2;
            this.SessionFactory.GetCurrentSession().Save(book2);

            book1.Name = "JEE";
            shipping1 = book1.Shipping;
            shipping1.City = "UK";
            this.SessionFactory.GetCurrentSession().Merge(book1);

            IList<Book> books = this.SessionFactory.GetCurrentSession().CreateQuery("select book from Book book").List<Book>();
            Assert.AreEqual(books.Count, 2);
            
            this.SessionFactory.GetCurrentSession().Delete(book2);

            books = this.SessionFactory.GetCurrentSession().CreateQuery("select book from Book book").List<Book>();
            Assert.AreEqual(books.Count, 1);
        }
    }
}
