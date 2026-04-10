using com.company.spancy.backend.dto;
using com.company.spancy.backend.entity;
using Spring.Validation;

namespace com.company.spancy.backend.service.impl
{
    public class PersonService : BaseService<PersonDto, Person>, IPersonService
    {
        public object CreatePersonTX([Validated("personValidator")] PersonDto dto)
        {
            return this.CreateTX(dto);
        }

        public object RemovePersonTX(long id)
        {
            Person person = this.Dao.LoadEntity(id);
            this.Dao.DeleteEntity(person);
            return null;
        }

        public object UpdatePersonTX([Validated("personValidator")] PersonDto dto)
        {
            return this.UpdateTX(dto);
        }
    }
}