using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreSolution.Dto.Entity
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuDto: BaseEntityDto
    {
        [Required]
        [Display(Name = "菜单名称")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "菜单名称")]
        public string Url { get; set; }

        [Display(Name = "自定义数据")]
        public string CustomData { get; set; }

        [Display(Name = "菜单图标")]
        public string Icon { get; set; }

        [Display(Name = "菜单类名称")]
        public string ClassName { get; set; }

        [Display(Name = "上级菜单Id")]
        public Guid? ParentId { get; set; }
    }

    /// <summary>
    /// 菜单有序列表展示
    /// </summary>
    public class MenuDtoView: MenuDto
    {
        public MenuDtoView()
        {
            this.ListMenuDto = new List<MenuDtoView>();
        }
        public List<MenuDtoView> ListMenuDto { get; set; }
    }

}
