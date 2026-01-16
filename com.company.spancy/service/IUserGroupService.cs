using com.company.spancy.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service
{
    public interface IUserGroupService : IBaseService<UserGroupDto>
    {
        object RemoveTX(UserGroupDto userGroupDto);
        object IsPresentRO(UserGroupDto userGroupDto);
    }
}
