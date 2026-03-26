using com.company.spancy.backend.dao;
using com.company.spancy.backend.entity;

namespace com.company.spancy.test.dao
{
    [TestClass]
    public class StudentDaoTests : BaseTest
    {
        public IBaseDao<Student> StudentDao { get; set; }

        [TestMethod]
        public void GetAllTest()
        {
            Assert.IsTrue(this.StudentDao.LoadAllEntities().Count > 0);
        }

        [TestMethod]
        public void InsertTest()
        {
            int initialCount = this.StudentDao.LoadAllEntities().Count;

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

            Assert.AreEqual(initialCount + 4, this.StudentDao.LoadAllEntities().Count);
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Student student1 = new Student();
            student1.Name = "Alex";
            Student mentor1 = new Student();
            mentor1.Name = "Fred";
            student1.Mentor = mentor1;
            long id = this.StudentDao.SaveEntity(student1);

            Student student2 = this.StudentDao.LoadEntity(id);
            Assert.AreEqual("Alex", student2.Name);
            Assert.AreEqual("Fred", student2.Mentor.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int initialCount = this.StudentDao.LoadAllEntities().Count;

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
            long student2Id = this.StudentDao.SaveEntity(student2);
            Assert.AreEqual(initialCount + 4, this.StudentDao.LoadAllEntities().Count);

            Student tempStudent = this.StudentDao.LoadEntity(student2Id);
            this.StudentDao.DeleteEntity(tempStudent);
            Assert.AreEqual(initialCount + 3, this.StudentDao.LoadAllEntities().Count);
            Assert.IsNull(this.StudentDao.LoadEntity(student2Id));
        }

        [TestMethod]
        public void UpdateTest()
        {
            Student student1 = new Student();
            student1.Name = "Alex";
            Student mentor1 = new Student();
            mentor1.Name = "Fred";
            student1.Mentor = mentor1;
            long id = this.StudentDao.SaveEntity(student1);
            Assert.IsTrue(this.StudentDao.LoadAllEntities().Count > 0);

            Student tempStudent = this.StudentDao.LoadEntity(id);
            tempStudent.Name = "Alex James";
            Student tempMentor = tempStudent.Mentor;
            tempMentor.Name = "Fred James";
            tempStudent.Mentor = tempMentor;
            this.StudentDao.UpdateEntity(tempStudent);

            Student updatedStudent = this.StudentDao.LoadEntity(id);
            Assert.AreEqual("Alex James", updatedStudent.Name);
            Assert.AreEqual("Fred James", updatedStudent.Mentor.Name);
        }
    }
}
