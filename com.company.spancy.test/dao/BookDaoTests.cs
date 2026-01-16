using com.company.spancy.dao;
using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class BookDaoTests : BaseTest
    {
        public IBookDao BookDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.BookDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Book book1 = new Book();
            book1.Name = "Java Book";
            Shipping shipping1 = new Shipping();
            shipping1.City = "UK";
            book1.Shipping = shipping1;
            this.BookDao.SaveEntity(book1);

            Book book2 = new Book();
            book2.Name = "Java SE Book";
            Shipping shipping2 = new Shipping();
            shipping2.City = "IN";
            book2.Shipping = shipping2;
            this.BookDao.SaveEntity(book2);

            Assert.AreEqual(2, this.BookDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Book book1 = new Book();
            book1.Name = "Java Book";
            Shipping shipping1 = new Shipping();
            shipping1.City = "UK";
            book1.Shipping = shipping1;
            this.BookDao.SaveEntity(book1);

            IList<Book> books = this.BookDao.LoadAllEntities();
            Book book = books[0];

            Book tempbook = this.BookDao.LoadEntity(book.Id);
            Assert.AreEqual(tempbook.Name, book.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Book book1 = new Book();
            book1.Name = "Java Book";
            Shipping shipping1 = new Shipping();
            shipping1.City = "UK";
            book1.Shipping = shipping1;
            this.BookDao.SaveEntity(book1);

            Book book2 = new Book();
            book2.Name = "Java SE Book";
            Shipping shipping2 = new Shipping();
            shipping2.City = "IN";
            book2.Shipping = shipping2;
            this.BookDao.SaveEntity(book2);

            Assert.AreEqual(2, this.BookDao.LoadAllEntities().Count);
            
            this.BookDao.DeleteEntity(book2);
            Assert.AreEqual(1, this.BookDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Book book1 = new Book();
            book1.Name = "Java Book";
            Shipping shipping1 = new Shipping();
            shipping1.City = "UK";
            book1.Shipping = shipping1;
            this.BookDao.SaveEntity(book1);

            Assert.AreEqual(1, this.BookDao.LoadAllEntities().Count);

            IList<Book> books = this.BookDao.LoadAllEntities();
            Book book2 = books[0];
            Shipping shipping = book2.Shipping;
            shipping.City = "IND";
            this.BookDao.UpdateEntity(book2);

            IList<Book> books1 = this.BookDao.LoadAllEntities();
            Book book3 = books1[0];
            Assert.AreEqual("IND", book3.Shipping.City);
        }
    }
}
