using com.company.spancy.dao;
using com.company.spancy.mapper;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using Spring.Aspects.Logging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.spancy.logging;
using Spring.Validation;

namespace com.company.spancy.service.impl
{
    public class BaseService<Dto,Entity> : IBaseService<Dto>
    {
        public virtual CustomLoggingAdvice Logging { get; set; }
        public virtual IList<string> ErrorMsges { get; set; }
        public virtual string Hql { get; set; }
        public virtual IBaseDao<Entity> Dao { get; set; }
        public virtual IBaseMapper<Dto,Entity> Mapper { get; set; }
        public virtual ResourceSetMessageSource MessageSource { get; set; }
        public virtual object CreateTX([Validated("dtoValidator")] Dto dto)
        {
            Entity entity = this.Mapper.Map(dto);
            if (this.Dao.FindByValueObject(this.Hql, entity).Count == 0)
            {
                return this.Dao.SaveEntity(entity);
            } 
            else
            {
                throw new Exception($"{MessageSource.GetMessage(ErrorMsges[0], new object[] { dto })}");
            }
        }
        public virtual IList<Dto> FindAllRO()
        {
            return this.Mapper.Map(this.Dao.LoadAllEntities());
        }
        public virtual Dto FindByIdRO(long id)
        {
            return this.Mapper.Map(this.Dao.LoadEntity(id));
        }
        public virtual object RemoveByIdTX(long id)
        {
            this.Dao.DeleteEntity(this.Dao.LoadEntity(id));
            return null;
        }
        public virtual object UpdateTX([Validated("dtoValidator")] Dto dto)
        {
            Entity entity = this.Mapper.Map(dto);
            if (this.Dao.FindByValueObject(this.Hql, entity).Count == 0)
            {
                this.Dao.UpdateEntity(entity);
                return null;
            }
            else
            {
                throw new Exception($"{MessageSource.GetMessage(ErrorMsges[0], new object[] { dto })}");
            }
        }
        public virtual IList<Dto> FindByValueObject(string hql, Dto dto)
        {
            return this.Mapper.Map(this.Dao.FindByValueObject(hql, this.Mapper.Map(dto)));
        }
    }
}
