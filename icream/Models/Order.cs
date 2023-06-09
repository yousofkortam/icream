﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace icream.Models
{
    public partial class Order
    {
        [Key]
        public int id { get; set; }
        public int? product_id { get; set; }
        public int? user_id { get; set; }

        [ForeignKey("product_id")]
        [InverseProperty("Orders")]
        public virtual Product product { get; set; }
        [ForeignKey("user_id")]
        [InverseProperty("Orders")]
        public virtual User user { get; set; }
    }
}