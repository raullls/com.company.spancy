using com.company.spancy.dto;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class BookServiceTests : BaseTest
    {
        public IBookService BookService { get; set; }
        
        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0, this.BookService.FindAllRO().Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            BookDto bookDto = new BookDto();
            bookDto.Name = "Java SE";
            bookDto.City = "IND";
            this.BookService.CreateTX(bookDto);

            Assert.AreEqual(1, this.BookService.FindAllRO().Count);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            BookDto bookDto = new BookDto();
            bookDto.Name = "Java SE";
            bookDto.City = "IND";
            this.BookService.CreateTX(bookDto);

            IList<BookDto> bookDtos = this.BookService.FindAllRO();
            BookDto bDto = bookDtos[0];

            BookDto bDto2 = this.BookService.FindByIdRO(bDto.Id);
            Assert.AreEqual("Java SE", bDto2.Name);
            Assert.AreEqual("IND", bDto2.City);
        }

        [TestMethod]
        public void RemoveTest()
        {
            BookDto bookDto = new BookDto();
            bookDto.Name = "Java SE";
            bookDto.City = "IND";
            this.BookService.CreateTX(bookDto);

            IList<BookDto> bookDtos = this.BookService.FindAllRO();
            Assert.AreEqual(1, bookDtos.Count);

            this.BookService.RemoveByIdTX(bookDtos[0].Id);

            bookDtos = this.BookService.FindAllRO();
            Assert.AreEqual(0, bookDtos.Count);
        }

        [TestMethod]
        public void EditTest()
        {
            BookDto bookDto = new BookDto();
            bookDto.Name = "Java SE";
            bookDto.City = "IND";
            this.BookService.CreateTX(bookDto);

            IList<BookDto> bookDtos = this.BookService.FindAllRO();
            Assert.AreEqual(1, bookDtos.Count);

            bookDtos[0].City = "CAN";
            this.BookService.UpdateTX(bookDtos[0]);

            bookDtos = this.BookService.FindAllRO();
            Assert.AreEqual("CAN", bookDtos[0].City);
        }
    }
}
