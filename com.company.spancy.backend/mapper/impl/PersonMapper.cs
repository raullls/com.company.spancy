using AutoMapper;
using com.company.spancy.backend.dto;
using com.company.spancy.backend.entity;

namespace com.company.spancy.backend.mapper.impl
{
    public class PersonMapper : BaseMapper<PersonDto, Person>, IBaseMapper<PersonDto, Person>
    {
        public PersonMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PersonDto, Person>()
                    .ForMember(dest => dest.Phones, opt => opt.MapFrom((src, dest, ctx) =>
                    {
                        IList<Phone> phones = new List<Phone>();
                        if (src.Numbers != null)
                        {
                            Person person = this.Dao.FindByValueObject(this.Hql, new Person { Id = src.Id, Name = src.Name }).SingleOrDefault() ?? new Person();
                            foreach (string number in src.Numbers)
                            {
                                phones.Add(person.Phones.Where(x => x.Number.Equals(number)).SingleOrDefault() ?? new Phone { Number = number });
                            }
                            this.Dao.Evict(person);
                        }
                        return phones;
                    }));

                cfg.CreateMap<Person, PersonDto>()
                    .ForMember(dest => dest.Numbers, opt => opt.MapFrom(src => src.Phones.Select(x => x.Number).ToList()));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}