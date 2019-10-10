using CoreSolution.Domain.Entity;
using CoreSolution.Dto.Entity;
using DapperCoreRepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.IService
{
   public interface IMenuService : IMsSqlRepositoryBase<Menu, MenuDto>, IServiceSupport
    {
        /// <summary>
        /// 获取相关菜单
        /// </summary>
        /// <returns></returns>
        List<MenuDtoView> GetMenuList(List<Guid> ListMenuId);
    }
}
