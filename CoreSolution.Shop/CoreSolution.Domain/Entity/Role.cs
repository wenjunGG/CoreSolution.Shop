using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreSolution.Domain.Entity
{
    /// <summary>
    /// 角色表
    /// </summary>
   public class Role:BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
