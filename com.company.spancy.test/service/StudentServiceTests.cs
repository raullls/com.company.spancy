using com.company.spancy.dto;
using com.company.spancy.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace com.company.spancy.test.service
{
    [TestClass]
    public class StudentServiceTests : BaseTest
    {
        public IStudentService StudentService { get; set; }

        [TestMethod]
        public void FindAllTest()
        {
            Assert.AreEqual(0, this.StudentService.FindAllRO().Count);
        }

        [TestMethod]
        public void CreateTest()
        {
            StudentDto studentDto = new StudentDto();
            studentDto.Name = "Alex";
            studentDto.MentorName = "Fred";
            this.StudentService.CreateStudentTX(studentDto);
            Assert.AreEqual(2, this.StudentService.FindAllRO().Count);
        }

        [TestMethod]
        public void FindByIdTest()
        {
            StudentDto studentDto = new StudentDto();
            studentDto.Name = "Alex";
            studentDto.MentorName = "Fred";
            this.StudentService.CreateStudentTX(studentDto);
            IList<StudentDto> studentDtos = this.StudentService.FindAllRO();
            StudentDto stDto = studentDtos[1];
            StudentDto stDto2 = this.StudentService.FindByIdRO(stDto.Id);
            Assert.IsNotNull(stDto2);
        }

        [TestMethod]
        public void RemoveTest()
        {
            StudentDto studentDto = new StudentDto();
            studentDto.Name = "Alex";
            studentDto.MentorName = "Fred";
            this.StudentService.CreateStudentTX(studentDto);

            StudentDto stuDto = new StudentDto();
            stuDto.Name = "Smith";
            stuDto.MentorName = "Flex";
            this.StudentService.CreateStudentTX(stuDto);
            Assert.AreEqual(4, this.StudentService.FindAllRO().Count);

            IList<StudentDto> studentDtos = this.StudentService.FindAllRO();
            StudentDto stDto = studentDtos[3];
            this.StudentService.RemoveByIdTX(stDto.Id);
            Assert.AreEqual(2, this.StudentService.FindAllRO().Count);
        }

        [TestMethod]
        public void EditTest()
        {
            StudentDto studentDto = new StudentDto();
            studentDto.Name = "Alex";
            studentDto.MentorName = "Fred";
            this.StudentService.CreateStudentTX(studentDto);
            IList<StudentDto> studentDtos = this.StudentService.FindAllRO();
            StudentDto sDto = studentDtos[1];
            sDto.Name = "James Alex";
            sDto.MentorName = "Fred James";
            this.StudentService.UpdateStudentTX(sDto);
            IList<StudentDto> students = this.StudentService.FindAllRO();
            Assert.AreEqual(3, students.Count);
        }
    }
}
