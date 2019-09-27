using AutoMapper;
using CoreSolution.Domain;
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
            CreateMap<UserInfoDto, Userinfo_t>();
            CreateMap<Userinfo_t, UserInfoDto>();
        }
    }
}
