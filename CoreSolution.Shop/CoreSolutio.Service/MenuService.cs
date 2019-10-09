using AutoMapper;
using CoreSolution.Domain.Entity;
using CoreSolution.Dto.Entity;
using CoreSolution.IService;
using DapperCoreRepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreSolution.Service
{
    public class MenuService : MsSqlRepositoryBase<Menu, MenuDto>, IMenuService
    {
        public List<MenuDtoView> GetMenuList()
        {
            var result = base.GetEntityDtoList();
            List<MenuDtoView> ListMenu = new List<MenuDtoView>();


            //获取一级目录
            var firstResult = result.Where(t => !t.ParentId.HasValue).OrderBy(t => t.Sort).ToList();
            //去掉一级目录的数据
            var DataResult = result.Where(t => t.ParentId.HasValue).ToList();

            foreach (var item in firstResult)
            {
                MenuDtoView menuView = new MenuDtoView();
                menuView = Mapper.Map<MenuDtoView>(item);

                var SecOrThreeMenuList = GetMenuList(item.Id, DataResult);
                if (SecOrThreeMenuList.Count > 0)
                {
                    menuView.ListMenuDto = SecOrThreeMenuList;
                }
                ListMenu.Add(menuView);
            }

            return ListMenu;
        }

        /// <summary>
        /// 获取二级以及三级菜单
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="ListMenuDto"></param>
        /// <returns></returns>
        private List<MenuDtoView> GetMenuList(Guid ParentId, List<MenuDto> ListMenuDto)
        {
            List<MenuDtoView> List = new List<MenuDtoView>();
            var SecListMenuDto = ListMenuDto.Where(t => t.ParentId == ParentId).OrderBy(t => t.Sort).ToList();
            foreach (var item in SecListMenuDto)
            {
                MenuDtoView menuView = new MenuDtoView();
                menuView = Mapper.Map<MenuDtoView>(item);
                var ThreeMenuDto = Mapper.Map<List<MenuDtoView>>(ListMenuDto.Where(t => t.ParentId == item.Id).OrderBy(t => t.Sort).ToList());
                if (ThreeMenuDto.Count > 0)
                {
                    menuView.ListMenuDto = ThreeMenuDto;
                }
                List.Add(menuView);
            }
            return List;
        }
    }
}
