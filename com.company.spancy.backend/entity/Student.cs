namespace com.company.spancy.backend.entity
{
    public class Student
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Student Mentor { get; set; }
        public virtual IList<Student> Mentees { get; set; }
    }
}