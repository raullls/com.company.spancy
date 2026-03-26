using com.company.spancy.backend.dto;

namespace com.company.spancy.backend.service
{
    public interface IStudentService : IBaseService<StudentDto>
    {
        object CreateStudentTX(StudentDto dto);
        object UpdateStudentTX(StudentDto dto);
        object RemoveStudentTX(long id);
    }
}