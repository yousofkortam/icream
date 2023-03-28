﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace icream.Models
{
    [Table("Clients_say")]
    public partial class Clients_say
    {
        [Key]
        public int id { get; set; }
        [StringLength(50)]
        public string name { get; set; }
        [StringLength(50)]
        public string job_name { get; set; }
        [StringLength(500)]
        public string review { get; set; }
        public int? user_id { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string image { get; set; }

        [ForeignKey("user_id")]
        [InverseProperty("Clients_says")]
        public virtual User user { get; set; }
    }
}