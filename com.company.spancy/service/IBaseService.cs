using com.company.spancy.mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service
{
    public interface IBaseService<Dto>
    {
        object CreateTX(Dto dto);
        IList<Dto> FindAllRO();
        Dto FindByIdRO(long id);
        object RemoveByIdTX(long id);
        object UpdateTX(Dto dto);
    }
}
