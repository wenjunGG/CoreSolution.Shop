using AutoMapper;
using CoreSolution.Domain;
using CoreSolution.Domain.Entity;
using CoreSolution.Dto.Entity;
using CoreSolution.IService;
using DapperCoreRepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolutio.Service
{
    public class UserInfoService : MsSqlRepositoryBase<UserInfo, UserInfoDto>, IUserInfoService
    {
        
    }
}
