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
    public class StudentDaoTests : BaseTest
    {
        public IStudentDao StudentDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.AreEqual(0, this.StudentDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Student student1 = new Student();
            student1.Name = "Alex";
            Student mentor1 = new Student();
            mentor1.Name = "Fred";
            student1.Mentor = mentor1;
            this.StudentDao.SaveEntity(student1);

            Student student2 = new Student();
            student2.Name = "Michel";
            Student mentor2 = new Student();
            mentor2.Name = "Mac";
            student2.Mentor = mentor2;
            this.StudentDao.SaveEntity(student2);

            Assert.AreEqual(4, this.StudentDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Student student1 = new Student();
            student1.Name = "Alex";
            Student mentor1 = new Student();
            mentor1.Name = "Fred";
            student1.Mentor = mentor1;
            this.StudentDao.SaveEntity(student1);

            IList<Student> students = this.StudentDao.LoadAllEntities();
            Student student = students[1];
            Student student2 = this.StudentDao.LoadEntity(student.Id);
            Assert.AreEqual("Alex", student2.Name);
            Assert.AreEqual("Fred", student2.Mentor.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Student student1 = new Student();
            student1.Name = "Alex";
            Student mentor1 = new Student();
            mentor1.Name = "Fred";
            student1.Mentor = mentor1;
            this.StudentDao.SaveEntity(student1);

            Student student2 = new Student();
            student2.Name = "Michel";
            Student mentor2 = new Student();
            mentor2.Name = "Mac";
            student2.Mentor = mentor2;
            this.StudentDao.SaveEntity(student2);
            Assert.AreEqual(4, this.StudentDao.LoadAllEntities().Count);

            IList<Student> students = this.StudentDao.LoadAllEntities();
            Student tempStudent = students[3];
            this.StudentDao.DeleteEntity(tempStudent);
            Assert.AreEqual(2, this.StudentDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Student student1 = new Student();
            student1.Name = "Alex";
            Student mentor1 = new Student();
            mentor1.Name = "Fred";
            student1.Mentor = mentor1;
            this.StudentDao.SaveEntity(student1);
            Assert.AreEqual(2, this.StudentDao.LoadAllEntities().Count);

            IList<Student> students = this.StudentDao.LoadAllEntities();
            Student tempStudent = students[1];
            tempStudent.Name = "Alex James";
            Student tempMentor = tempStudent.Mentor;
            tempMentor.Name = "Fred James";
            tempStudent.Mentor = tempMentor;
            this.StudentDao.UpdateEntity(tempStudent);
            Assert.AreEqual(2, this.StudentDao.LoadAllEntities().Count);
        }
    }
}
