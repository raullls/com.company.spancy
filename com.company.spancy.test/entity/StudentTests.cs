using com.company.spancy.entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.entity
{
    [TestClass]
    public class StudentTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Student student1 = new Student();
            student1.Name = "Alex";
            Student mentor1 = new Student();
            mentor1.Name = "Fred";
            student1.Mentor = mentor1;
            this.SessionFactory.GetCurrentSession().Save(student1);
            Student student2 = new Student();
            student2.Name = "Michel";
            Student mentor2 = new Student();
            mentor2.Name = "Mac";
            student2.Mentor = mentor2;
            this.SessionFactory.GetCurrentSession().Save(student2);
            IList<Student> students = this.SessionFactory.GetCurrentSession().CreateQuery("select student from Student student").List<Student>();
            Assert.AreEqual(4, students.Count);
            Student tempStudent = students[1];
            tempStudent.Name = "Alex James";
            Student tempmentor = tempStudent.Mentor;
            tempmentor.Name = "Fred James";
            this.SessionFactory.GetCurrentSession().Merge(tempStudent);
            students = this.SessionFactory.GetCurrentSession().CreateQuery("select student from Student student").List<Student>();
            Assert.AreEqual(4, students.Count);
            tempStudent = students[3];
            this.SessionFactory.GetCurrentSession().Delete(tempStudent);
            students = this.SessionFactory.GetCurrentSession().CreateQuery("select student from Student student").List<Student>();
            Assert.AreEqual(2, students.Count);
        }
    }
}
