using CoreSolution.Domain.Entity;
using CoreSolution.Dto.Entity;
using DapperCoreRepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.IService
{
   public interface IUserRoleService : IMsSqlRepositoryBase<UserRole, UserRoleDto>, IServiceSupport
    {

    }
}
