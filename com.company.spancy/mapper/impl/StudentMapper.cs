using AutoMapper;
using com.company.spancy.dto;
using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.mapper.impl
{
    public class StudentMapper : BaseMapper<StudentDto, Student>, IBaseMapper<StudentDto, Student>
    {
        public StudentMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StudentDto, Student>()
                    .ForMember(dest => dest.Mentor, opt => opt.MapFrom((src, dest, ctx) => 
                    {
                        if (string.IsNullOrEmpty(src.MentorName))
                            return null;

                        Student mentor = this.Dao.FindByValueObject(this.Hql, new Student { Name = src.MentorName }).SingleOrDefault();
                        this.Dao.Evict(mentor);
                        return mentor ?? new Student { Name = src.MentorName }; 
                    }));

                cfg.CreateMap<Student, StudentDto>()
                    .ForMember(dest => dest.MentorName, opt => opt.MapFrom(src => src.Mentor != null ? src.Mentor.Name : null));
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }
    }
}
