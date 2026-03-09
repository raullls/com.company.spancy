using com.company.spancy.backend.dao;
using com.company.spancy.backend.entity;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class BookDaoTests : BaseTest
    {
        public IBaseDao<Book> BookDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.IsTrue(this.BookDao.LoadAllEntities().Count > 0);
        }

        [TestMethod]
        public void InsertTest()
        {
            Book firstBook = new Book();
            firstBook.Name = "Spring in Action";
            Shipping shipping1 = new Shipping();
            shipping1.City = "UK";
            firstBook.Shipping = shipping1;
            this.BookDao.SaveEntity(firstBook);

            Book secondBook = new Book();
            secondBook.Name = "Hibernate in Action";
            Shipping shipping2 = new Shipping();
            shipping2.City = "IN";
            secondBook.Shipping = shipping2;
            this.BookDao.SaveEntity(secondBook);

            Assert.IsTrue(this.BookDao.LoadAllEntities().Count > 0);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Book bookToSave = new Book();
            bookToSave.Name = "Spring in Action";
            Shipping shipping1 = new Shipping();
            shipping1.City = "UK";
            bookToSave.Shipping = shipping1;
            long id = this.BookDao.SaveEntity(bookToSave);

            IList<Book> books = this.BookDao.LoadAllEntities();
            Book firstBook = books[0];

            Book loadedBook = this.BookDao.LoadEntity(id);
            Assert.AreEqual("Spring in Action", loadedBook.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Book firstBook = new Book();
            firstBook.Name = "Spring in Action";
            Shipping shipping1 = new Shipping();
            shipping1.City = "UK";
            firstBook.Shipping = shipping1;
            this.BookDao.SaveEntity(firstBook);

            Book secondBook = new Book();
            secondBook.Name = "Hibernate in Action";
            Shipping shipping2 = new Shipping();
            shipping2.City = "IN";
            secondBook.Shipping = shipping2;
            this.BookDao.SaveEntity(secondBook);

            Assert.IsTrue(this.BookDao.LoadAllEntities().Count > 0);

            this.BookDao.DeleteEntity(secondBook);

            Assert.IsTrue(this.BookDao.LoadAllEntities().Count > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Book bookToSave = new Book();
            bookToSave.Name = "Spring in Action";
            Shipping shipping1 = new Shipping();
            shipping1.City = "UK";
            bookToSave.Shipping = shipping1;
            this.BookDao.SaveEntity(bookToSave);

            Assert.IsTrue(this.BookDao.LoadAllEntities().Count > 0);

            IList<Book> books = this.BookDao.LoadAllEntities();
            Book book = books[0];
            book.Name = "Head First Design Patterns";
            Shipping shipping = book.Shipping;
            shipping.City = "IND";

            this.BookDao.UpdateEntity(book);

            IList<Book> updatedBooks = this.BookDao.LoadAllEntities();
            Book updatedBook = updatedBooks[0];
            Assert.AreEqual("Head First Design Patterns", updatedBook.Name);
            Assert.AreEqual("IND", updatedBook.Shipping.City);
        }
    }
}
