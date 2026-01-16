using com.company.spancy.dto;
using com.company.spancy.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service.impl
{
    public class GroupService : BaseService<GroupDto, Group>, IGroupService
    {
        public virtual IUserGroupService UserGroupService { get; set; }
        public object RemoveGroupByIdTX(long id)
        {
            IList<UserGroupDto> userGroupsDto = ((this.UserGroupService.FindAllRO() as Response).Data as IList<UserGroupDto>).Where(x => x.GroupDto.Id == id).ToList();
            foreach (UserGroupDto userGroupDto in userGroupsDto)
            {
                this.UserGroupService.RemoveTX(userGroupDto);
            }
            return base.RemoveByIdTX(id);
        }
    }
}
