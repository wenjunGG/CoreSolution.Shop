using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto.Entity
{
   public class UserInfoDto
    {
        /// <summary>
        /// Id
        /// </summary>    
        //[Required]
        //[Display(Name = "Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// UserName
        /// </summary>    
        //[StringLength(50)]
        //[Display(Name = "UserName")]
        public string UserName { get; set; }
    }
}
