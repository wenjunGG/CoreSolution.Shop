using CoreSolution.Domain.Entity;
using CoreSolution.Dto.Entity;
using CoreSolution.IService;
using DapperCoreRepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Service
{
  public  class UserRoleService : MsSqlRepositoryBase<UserRole, UserRoleDto>, IUserRoleService
    {

    }
}
