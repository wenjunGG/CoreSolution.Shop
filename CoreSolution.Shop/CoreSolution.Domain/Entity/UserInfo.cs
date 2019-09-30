using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreSolution.Domain.Entity
{
   public class UserInfo: BaseEntity
    {
        [Required]
        [Display(Name ="用户名")]
        public string UserName { get; set; }

        [Display(Name = "年龄")]
        public int UserAge { get; set; }

        [Required]
        [Display(Name = "用户密码")]
        public string UserPwd { get; set; }
    }
}
