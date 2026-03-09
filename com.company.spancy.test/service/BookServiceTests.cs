using com.company.spancy.backend.dto;
using com.company.spancy.backend.service;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class BookServiceTests : BaseTest
    {
        public IBookService BookService { get; set; } = null!;

        public BookServiceTests() : base(true) {}

        [TestMethod]
        public void FindAllTest()
        {
            Assert.IsTrue(this.BookService.FindAllRO().Count > 0);
        }

        [TestMethod]
        public void CreateBookTest()
        {
            BookDto createBookTxDto = new BookDto
            {
                Name = "Spring in Action",
                City = "Madrid"
            };
            long createdWithSpecificMethod = (long)this.BookService.CreateBookTX(createBookTxDto);

            BookDto fromId = this.BookService.FindByIdRO(createdWithSpecificMethod);
            Assert.AreEqual("Spring in Action", fromId.Name);
            Assert.AreEqual("Madrid", fromId.City);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            BookDto createTxDto = new BookDto
            {
                Name = "Hibernate in Action",
                City = "Barcelona"
            };
            long createdWithBaseMethod = (long)this.BookService.CreateBookTX(createTxDto);

            BookDto fromId = this.BookService.FindByIdRO(createdWithBaseMethod);
            Assert.AreEqual("Hibernate in Action", fromId.Name);
            Assert.AreEqual("Barcelona", fromId.City);
        }

        [TestMethod]
        public void UpdateBookTXTest()
        {
            BookDto createBookTxDto = new BookDto
            {
                Name = "Spring in Action",
                City = "Madrid"
            };
            long createdWithSpecificMethod = (long)this.BookService.CreateBookTX(createBookTxDto);

            BookDto fromId = this.BookService.FindByIdRO(createdWithSpecificMethod);

            fromId.Name = "Spring in Practice";
            fromId.City = "Seville";
            this.BookService.UpdateBookTX(fromId);

            BookDto updated = this.BookService.FindByIdRO(createdWithSpecificMethod);
            Assert.AreEqual("Spring in Practice", updated.Name);
            Assert.AreEqual("Seville", updated.City);
        }

        [TestMethod]
        public void RemoveByIdTXTest()
        {
            BookDto createTxDto = new BookDto
            {
                Name = "Hibernate in Action",
                City = "Barcelona"
            };
            long createdWithBaseMethod = (long)this.BookService.CreateTX(createTxDto);

            this.BookService.RemoveByIdTX(createdWithBaseMethod);
            IList<BookDto> remaining = this.BookService.FindAllRO();
            Assert.IsTrue(remaining.All(book => book.Id != createdWithBaseMethod));
        }
    }
}
