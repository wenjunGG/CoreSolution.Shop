using AutoMapper;
using CoreSolution.Domain;
using CoreSolution.Domain.Entity;
using CoreSolution.Dto.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile.EntityMap
{
   public class UserInfoProfile: Profile,IProfile
    {
        public UserInfoProfile()
        {

            //用户
            CreateMap<UserInfoDto, UserInfo>();
            CreateMap<UserInfo, UserInfoDto>();

            //菜单
            CreateMap<MenuDto, Menu>();
            CreateMap<Menu, MenuDto>();
            CreateMap<MenuDto, MenuDtoView>();
            CreateMap<MenuDtoView, MenuDto>();
            
            //菜单角色
            CreateMap<MenuRoleDto, MenuRole>();
            CreateMap<MenuRole, MenuRoleDto>();

            //角色
            CreateMap<RoleDto, Role>();
            CreateMap<Role, RoleDto>();

            //用户角色
            CreateMap<UserRoleDto, UserRole>();
            CreateMap<UserRole, UserRoleDto>();


        }
    }
}
