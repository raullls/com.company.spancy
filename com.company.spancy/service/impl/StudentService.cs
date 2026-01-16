using com.company.spancy.dto;
using com.company.spancy.entity;
using Spring.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service.impl
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
