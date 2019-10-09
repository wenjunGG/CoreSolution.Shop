using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreSolution.Dto.Entity
{
    /// <summary>
    /// 角色
    /// </summary>
   public class RoleDto: BaseEntityDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
