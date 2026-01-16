using AutoMapper;
using com.company.spancy.dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.mapper.impl
{
    public class BaseMapper<Dto, Entity> : IBaseMapper<Dto, Entity>
    {
        protected IMapper mapper;
        protected IBaseDao<Entity> Dao;
        protected string Hql;
        
        public BaseMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Dto, Entity>();
                cfg.CreateMap<Entity, Dto>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        public Entity Map(Dto dto)
        {
            return this.mapper.Map<Entity>(dto);
        }

        public IList<Entity> Map(IList<Dto> dtos)
        {
            IList<Entity> result = new List<Entity>();
            foreach (Dto dto in dtos)
            {
                result.Add(this.mapper.Map<Entity>(dto));
            }
            return result;
        }

        public Dto Map(Entity entity)
        {
            return this.mapper.Map<Dto>(entity);
        }

        public IList<Dto> Map(IList<Entity> entities)
        {
            IList<Dto> result = new List<Dto>();
            foreach (Entity entity in entities)
            {
                result.Add(this.mapper.Map<Dto>(entity));
            }
            return result;
        }
    }
}
