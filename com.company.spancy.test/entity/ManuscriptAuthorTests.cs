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
    public class ManuscriptAuthorTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Manuscript manuscript1 = new Manuscript();
            manuscript1.Name = "Test Driven Application Development with Spring and Hibernate";

            Author author1 = new Author();
            author1.Name = "Amritendu De";
            this.SessionFactory.GetCurrentSession().Save(author1);

            ManuscriptAuthor manuscriptAuthor1 = new ManuscriptAuthor();
            manuscriptAuthor1.Manuscript = manuscript1;
            manuscriptAuthor1.Author = author1;
            manuscriptAuthor1.Publisher = "Createspace";
            manuscript1.ManuscriptAuthors.Add(manuscriptAuthor1);

            Manuscript manuscript2 = new Manuscript();
            manuscript2.Name = "The Lord of the Rings";
            
            Author author2 = new Author();
            author2.Name = "J.R.R.Tolkien";
            this.SessionFactory.GetCurrentSession().Save(author2);
            
            ManuscriptAuthor manuscriptAuthor2 = new ManuscriptAuthor();
            manuscriptAuthor2.Manuscript = manuscript2;
            manuscriptAuthor2.Author = author2;
            manuscriptAuthor2.Publisher = "Createspace";
            manuscript2.ManuscriptAuthors.Add(manuscriptAuthor2);
            
            this.SessionFactory.GetCurrentSession().Save(manuscript1);
            this.SessionFactory.GetCurrentSession().Save(manuscript2);
            
            author1.Name = "Amish Tripathi";
            this.SessionFactory.GetCurrentSession().Merge(author1);
            
            IList<ManuscriptAuthor> list = this.SessionFactory.GetCurrentSession().CreateQuery("from ManuscriptAuthor").List<ManuscriptAuthor>();
            Assert.AreEqual(2, list.Count());
            
            IList<Manuscript> manuscriptList = this.SessionFactory.GetCurrentSession().CreateQuery("from Manuscript").List<Manuscript>();
            Manuscript tmpManuscript = manuscriptList[0];
            foreach (ManuscriptAuthor manuscriptAuthor in tmpManuscript.ManuscriptAuthors.ToList())
            {
                tmpManuscript.ManuscriptAuthors.Remove(manuscriptAuthor);
                //this.SessionFactory.GetCurrentSession().Delete(manuscriptAuthor);
            }
            this.SessionFactory.GetCurrentSession().Merge(tmpManuscript);
            IList<ManuscriptAuthor> list2 = this.SessionFactory.GetCurrentSession().CreateQuery("from ManuscriptAuthor").List<ManuscriptAuthor>();
            Assert.AreEqual(1, list2.Count());
        }
    }
}
