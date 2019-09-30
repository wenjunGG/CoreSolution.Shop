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
            CreateMap<UserInfoDto, UserInfo>();
            CreateMap<UserInfo, UserInfoDto>();
        }
    }
}
