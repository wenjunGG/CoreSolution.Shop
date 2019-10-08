using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreSolution.Dto.Entity
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRole: BaseEntityDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid RoleId { get; set; }
    }
}
