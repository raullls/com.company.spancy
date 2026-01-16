using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.mapper
{
    public interface IBaseMapper<Dto,Entity>
    {
        Entity Map(Dto dto);
        Dto Map(Entity entity);
        IList<Entity> Map(IList<Dto> dtos);
        IList<Dto> Map(IList<Entity> entities);
    }
}
