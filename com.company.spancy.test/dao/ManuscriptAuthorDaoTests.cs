using com.company.spancy.dao;
using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class ManuscriptAuthorDaoTests : BaseTest
    {
        public IManuscriptDao ManuscriptDao { get; set; }
        public IAuthorDao AuthorDao { get; set; }
        public IManuscriptAuthorDao ManuscriptAuthorDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.ManuscriptAuthorDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void IsPresentTest()
        {
            bool status = false;
            Manuscript manuscript = new Manuscript();
            manuscript.Name = "Test Driven Application Development with Spring and Hibernate";

            Author author = new Author();
            author.Name = "Amritendu De";
            this.AuthorDao.SaveEntity(author);

            ManuscriptAuthor manuscriptAuthor = new ManuscriptAuthor();
            manuscriptAuthor.Manuscript = manuscript;
            manuscriptAuthor.Author = author;
            manuscriptAuthor.Publisher = "Createspace";

            manuscript.ManuscriptAuthors.Add(manuscriptAuthor);
            this.ManuscriptDao.SaveEntity(manuscript);

            Manuscript manuscript2 = this.ManuscriptDao.LoadAllEntities()[0];
            Author author2 = this.AuthorDao.LoadAllEntities()[0];

            IList<ManuscriptAuthor> manuscriptAuthorList = this.ManuscriptAuthorDao.IsPresent(manuscript2.Id, author2.Id);
            if (null != manuscriptAuthorList)
            {
                if (manuscriptAuthorList.Count>0)
                {
                    status = true;
                }
            }
            Assert.IsTrue(status);
        }
    }
}
