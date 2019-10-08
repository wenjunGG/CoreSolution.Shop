using CoreSolution.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreSolution.Domain.Entity
{
    /// <summary>
    /// 用户表
    /// </summary>
   public class UserInfo: BaseEntity
    {
        [Required]
        [Display(Name ="用户名")]
        public string UserName { get; set; }
        
        [Display(Name = "真实姓名")]
        public string RealName { get; set; }
        
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        
        [Display(Name = "邮箱确认")]
        public bool IsEmailConfirmed { get; set; }

        [Required]
        [Display(Name = "手机号码")]
        public string PhoneNum { get; set; }

   
        [Display(Name = "手机号码确认")]
        public bool IsPhoneNumConfirmed { get; set; }

        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "用户类型")]
        public UserType UserType { get; set; }
        
        [Display(Name = "性别")]
        public bool Sex { get; set; }

        [Display(Name = "头像")]
        public string Picture { get; set; }

        [Display(Name = "年龄")]
        public int Age { get; set; }
    }
}
