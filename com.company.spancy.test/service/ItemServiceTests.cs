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
    public class ItemServiceTests : BaseTest
    {
        public IItemService ItemService { get; set; }
        public ItemServiceTests() : base(true) { }
        
        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0, this.ItemService.FindAllRO().Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            ItemDto itemDto = new ItemDto();
            itemDto.Name = "Book";
            IList<string> list = new List<string>();
            list.Add("Java");
            list.Add("JEE");
            list.Add("Spring");
            itemDto.FeatureList = list;
            this.ItemService.CreateItemTX(itemDto);
            Assert.AreEqual(1, this.ItemService.FindAllRO().Count());
        }

        [TestMethod]
        public void FindByIdTest()
        {
            ItemDto itemDto = new ItemDto();
            itemDto.Name = "Book";
            IList<string> list = new List<string>();
            list.Add("Java");
            list.Add("JEE");
            list.Add("Spring");
            itemDto.FeatureList = list;
            long? id = this.ItemService.CreateItemTX(itemDto) as long?;
            
            ItemDto dto1 = this.ItemService.FindByIdRO(id.Value);
            Assert.AreEqual("Book", dto1.Name);
            Assert.AreEqual(3, dto1.FeatureList.Count());
        }

        [TestMethod]
        public void RemoveTest()
        {
            ItemDto itemDto1 = new ItemDto();
            itemDto1.Name = "Book";
            IList<string> list1 = new List<string>();
            list1.Add("Java");
            list1.Add("JEE");
            list1.Add("Spring");
            itemDto1.FeatureList = list1;
            this.ItemService.CreateItemTX(itemDto1);
            ItemDto itemDto2 = new ItemDto();
            itemDto2.Name = "Bags";
            IList<string> list2 = new List<string>();
            list2.Add("SkyBags");
            list2.Add("WildCraft");
            list2.Add("Puma");
            itemDto2.FeatureList = list2;
            long id = (long)this.ItemService.CreateItemTX(itemDto2);
            Assert.AreEqual(2, this.ItemService.FindAllRO().Count());
            this.ItemService.RemoveByIdTX(id);
            Assert.AreEqual(1, this.ItemService.FindAllRO().Count());
        }

        [TestMethod]
        public void EditTest()
        {
            ItemDto itemDto = new ItemDto();
            itemDto.Name = "Book";
            IList<string> list = new List<string>();
            list.Add("Java");
            list.Add("JEE");
            list.Add("Spring");
            itemDto.FeatureList = list;
            long id = (long)this.ItemService.CreateItemTX(itemDto);
            Assert.AreEqual(1, this.ItemService.FindAllRO().Count());
            ItemDto dto = this.ItemService.FindByIdRO(id);
            list = new List<string>();
            list.Add("EJB 3.0");
            dto.FeatureList = list;
            this.ItemService.UpdateItemTX(dto);
            Assert.AreEqual(1, this.ItemService.FindAllRO()[0].FeatureList.Count());
        }
    }
}
