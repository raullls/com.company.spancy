using com.company.spancy.dto;
using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service
{
    public interface IStudentService : IBaseService<StudentDto>
    {
        object CreateStudentTX(StudentDto dto);
        object UpdateStudentTX(StudentDto dto);
        object RemoveStudentTX(long id);
    }
}
