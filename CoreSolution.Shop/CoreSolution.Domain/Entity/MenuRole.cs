using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreSolution.Domain.Entity
{
    /// <summary>
    /// 菜单角色表
    /// </summary>
    public class MenuRole: BaseEntity
    {
        [Required]
        [Display(Name ="菜单Id")]
        public Guid MenuId { get; set; }

        [Required]
        [Display(Name = "角色Id")]
        public Guid RoleId { get; set; }
    }
}
