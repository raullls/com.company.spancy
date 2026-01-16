using com.company.spancy.dao;
using com.company.spancy.dto;
using com.company.spancy.entity;
using com.company.spancy.mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.company.spancy.service.impl
{
    public class UserGroupService : IUserGroupService
    {
        public IUserDao UserDao { get; set; }
        public IGroupDao GroupDao { get; set; }
        public IUserGroupDao UserGroupDao { get; set; }
        public IBaseMapper<UserDto, User> UserMapper { get; set; }
        public IBaseMapper<GroupDto, Group> GroupMapper { get; set; }

        public object CreateTX(UserGroupDto dto)
        {
            User user = this.UserDao.LoadEntity(dto.UserDto.Id);
            Group group = this.GroupDao.LoadEntity(dto.GroupDto.Id);
            user.Groups.Add(group);
            return this.UserDao.SaveEntity(user);
        }

        public IList<UserGroupDto> FindAllRO()
        {
            IList<UserGroupDto> userGroupDtos = new List<UserGroupDto>();
            IList<User> userList = this.UserGroupDao.GetAll();
            foreach (User user in userList)
            {
                UserDto userDto = this.UserMapper.Map(user);
                foreach (Group group in user.Groups)
                {
                    UserGroupDto userGroupDto = new UserGroupDto();
                    userGroupDto.UserDto = userDto;
                    GroupDto groupDto = this.GroupMapper.Map(group);
                    userGroupDto.GroupDto = groupDto;
                    userGroupDtos.Add(userGroupDto);
                }
            }
            return userGroupDtos;
        }

        public UserGroupDto FindByIdRO(long id)
        {
            throw new NotImplementedException();
        }

        public object IsPresentRO(UserGroupDto userGroupDto)
        {
            bool status = false;
            IList<User> userList = this.UserGroupDao.IsPresent(userGroupDto.UserDto.Id, userGroupDto.GroupDto.Id);
            if (null != userList)
            {
                if (userList.Count() > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public object RemoveTX(UserGroupDto userGroupDto)
        {
            User user = this.UserDao.LoadEntity(userGroupDto.UserDto.Id);
            Group group = this.GroupDao.LoadEntity(userGroupDto.GroupDto.Id);
            user.Groups.Remove(group);
            this.UserDao.UpdateEntity(user);
            return 0;
        }

        public object RemoveByIdTX(long id)
        {
            throw new NotImplementedException();
        }

        public object UpdateTX(UserGroupDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
