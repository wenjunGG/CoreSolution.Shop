﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreSolution.Domain.Entity
{
    /// <summary>
    /// 菜单表
    /// </summary>
   public class Menu: BaseEntity
    {
        [Required]
        [Display(Name ="菜单名称")]
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
       
        [Display(Name ="上级菜单Id")]
        public Guid? ParentId { get; set; }
    }
}
