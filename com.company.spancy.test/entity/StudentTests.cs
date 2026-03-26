using com.company.spancy.backend.entity;

namespace com.company.spancy.test.entity
{
    [TestClass]
    public class StudentTests : BaseTest
    {
        [TestMethod]
        public void CRUDTest()
        {
            Student s1 = new Student();
            s1.Name = "A";
            Student mentor1 = new Student();
            mentor1.Name = "Mentor A";
            s1.Mentor = mentor1;
            this.SessionFactory.GetCurrentSession().Save(s1);

            Student s2 = new Student();
            s2.Name = "B";
            Student mentor2 = new Student();
            mentor2.Name = "Mentor B";
            s2.Mentor = mentor2;
            this.SessionFactory.GetCurrentSession().Save(s2);

            s1.Name = "C";
            mentor1 = s1.Mentor;
            mentor1.Name = "Mentor C";
            this.SessionFactory.GetCurrentSession().Merge(s1);

            IList<Student> list = this.SessionFactory.GetCurrentSession().CreateQuery("from Student").List<Student>();
            Assert.IsTrue(list.Count > 0);

            this.SessionFactory.GetCurrentSession().Delete(s2);
            list = this.SessionFactory.GetCurrentSession().CreateQuery("from Student").List<Student>();
            Assert.IsTrue(list.Count > 0);
        }
    }
}
