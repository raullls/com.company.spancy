using com.company.spancy.backend.dto;

namespace com.company.spancy.backend.service
{
    public interface IPersonService : IBaseService<PersonDto>
    {
        object CreatePersonTX(PersonDto dto);
        object UpdatePersonTX(PersonDto dto);
        object RemovePersonTX(long id);
    }
}