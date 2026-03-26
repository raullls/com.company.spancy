using com.company.spancy.backend.dto;
using com.company.spancy.backend.entity;
using Spring.Validation;

namespace com.company.spancy.backend.service.impl
{
    public class StudentService : BaseService<StudentDto, Student>, IStudentService
    {
        public object CreateStudentTX([Validated("studentValidator")] StudentDto dto)
        {
            Student student = this.Mapper.Map(dto);
            if (student.Mentor == null || student.Mentor.Id != 0)
            {
                return this.CreateTX(dto);
            }
            else
            {
                throw new Exception($"{MessageSource.GetMessage(ErrorMsges[1], new object[] { dto })}");
            }
        }

        public object RemoveStudentTX(long id)
        {
            Student student = this.Dao.LoadEntity(id);
            foreach (Student mentee in student.Mentees.ToList())
            {
                mentee.Mentor = null;
                this.Dao.UpdateEntity(mentee);
            }
            this.Dao.DeleteEntity(student);
            return null;
        }

        public object UpdateStudentTX([Validated("studentValidator")] StudentDto dto)
        {
            Student student = this.Mapper.Map(dto);
            if (student.Mentor == null || student.Mentor.Id != 0)
            {
                return this.UpdateTX(dto);
            }
            else
            {
                throw new Exception($"{MessageSource.GetMessage(ErrorMsges[1], new object[] { dto })}");
            }
        }
    }
}