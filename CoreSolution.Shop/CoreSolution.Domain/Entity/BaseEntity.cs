﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreSolution.Domain.Entity
{
    /// <summary>
    /// 基础类
    /// </summary>
    public class BaseEntity
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
            this.IsDelete = false;
            this.CreateDateTime = DateTime.Now;
            this.CreateUserId = Guid.NewGuid();
        }

        [Required]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid CreateUserId { get; set; }
        [Required]
        public DateTime CreateDateTime { get; set; }

        public Guid? UpdateUserId { get; set; }

        public DateTime? UpdateDateTime { get; set; }
        [Required]
        public bool IsDelete { get; set; }

        public int Sort { get; set; }
         
        [MaxLength(2000)]
        public string Remark { get; set; }
    }
}
