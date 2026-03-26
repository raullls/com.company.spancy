using com.company.spancy.backend.dto;
using com.company.spancy.backend.service;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class StudentServiceTests : BaseTest
    {
        public IStudentService StudentService { get; set; } = null!;

        public StudentServiceTests() : base(true) {}

        [TestMethod]
        public void FindAllTest()
        {
            Assert.IsTrue(this.StudentService.FindAllRO().Count > 0);
        }

        [TestMethod]
        public void CreateStudentTest()
        {
            StudentDto mentorDto = new StudentDto
            {
                Name = "Mentor Alice"
            };
            this.StudentService.CreateTX(mentorDto);

            StudentDto dto = new StudentDto
            {
                Name = "Alice",
                MentorName = "Mentor Alice"
            };
            long id = (long)this.StudentService.CreateStudentTX(dto);

            StudentDto loaded = this.StudentService.FindByIdRO(id);
            Assert.AreEqual("Alice", loaded.Name);
            Assert.AreEqual("Mentor Alice", loaded.MentorName);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            StudentDto mentorDto = new StudentDto
            {
                Name = "Mentor Bob"
            };
            this.StudentService.CreateTX(mentorDto);

            StudentDto dto = new StudentDto
            {
                Name = "Bob",
                MentorName = "Mentor Bob"
            };
            long id = (long)this.StudentService.CreateStudentTX(dto);

            StudentDto loaded = this.StudentService.FindByIdRO(id);
            Assert.AreEqual("Bob", loaded.Name);
            Assert.AreEqual("Mentor Bob", loaded.MentorName);
        }

        [TestMethod]
        public void UpdateStudentTXTest()
        {
            StudentDto firstMentorDto = new StudentDto
            {
                Name = "Mentor Carol"
            };
            this.StudentService.CreateTX(firstMentorDto);

            StudentDto createDto = new StudentDto
            {
                Name = "Carol",
                MentorName = "Mentor Carol"
            };
            long id = (long)this.StudentService.CreateStudentTX(createDto);

            StudentDto secondMentorDto = new StudentDto
            {
                Name = "Mentor Carol Updated"
            };
            this.StudentService.CreateTX(secondMentorDto);

            StudentDto toUpdate = this.StudentService.FindByIdRO(id);
            toUpdate.Name = "Carol Updated";
            toUpdate.MentorName = "Mentor Carol Updated";

            this.StudentService.UpdateStudentTX(toUpdate);

            StudentDto updated = this.StudentService.FindByIdRO(id);
            Assert.AreEqual("Carol Updated", updated.Name);
            Assert.AreEqual("Mentor Carol Updated", updated.MentorName);
        }

        [TestMethod]
        public void RemoveStudentTXTest()
        {
            StudentDto mentorDto = new StudentDto
            {
                Name = "Mentor Dave"
            };
            long mentorId = (long)this.StudentService.CreateTX(mentorDto);

            StudentDto dto = new StudentDto
            {
                Name = "Dave",
                MentorName = "Mentor Dave"
            };
            long studentId = (long)this.StudentService.CreateStudentTX(dto);

            this.StudentService.RemoveStudentTX(mentorId);

            IList<StudentDto> remaining = this.StudentService.FindAllRO();
            Assert.IsTrue(remaining.All(student => student.Id != mentorId));

            StudentDto updatedStudent = this.StudentService.FindByIdRO(studentId);
            Assert.AreEqual("Dave", updatedStudent.Name);
            Assert.IsNull(updatedStudent.MentorName);
        }
    }
}
