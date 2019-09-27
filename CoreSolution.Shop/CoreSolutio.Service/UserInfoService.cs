using AutoMapper;
using CoreSolution.Domain;
using CoreSolution.Dto.Entity;
using CoreSolution.IService;
using DapperCoreRepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolutio.Service
{
    public class UserInfoService : MsSqlRepositoryBase<Userinfo_t, UserInfoDto>, IUserInfoService
    {
        
    }
}
