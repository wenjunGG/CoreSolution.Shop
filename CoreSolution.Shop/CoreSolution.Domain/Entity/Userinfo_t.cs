
/*
 * 本文件由根据实体插件自动生成，请勿更改
 * =========================== */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CoreSolution.Domain
{
    public class Userinfo_t
    {
        
        /// <summary>
        /// Id
        /// </summary>    
        [Required] 
        [Display(Name="Id")]
        public Guid Id{ get; set; }
        
        /// <summary>
        /// UserName
        /// </summary>    
        [StringLength(50)]
        [Display(Name="UserName")]
        public string UserName{ get; set; }
    }
}
