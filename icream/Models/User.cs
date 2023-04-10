﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace icream.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            Clients_says = new HashSet<Clients_say>();
            Contacts = new HashSet<Contact>();
            Orders = new HashSet<Order>();
        }

        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        public string name { get; set; }
        [Required]
        [StringLength(50)]
        public string username { get; set; }
        [Required]
        [StringLength(150)]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string email { get; set; }
        [Required]
        [StringLength(150)]
        [Unicode(false)]
        public string password { get; set; }
        [NotMapped]
        [Compare("password", ErrorMessage = "Passwords not matches")]
        public string confirm_password { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string image { get; set; }
        [StringLength(500)]
        public string address { get; set; }
        [StringLength(500)]
        public string bio { get; set; }
        [StringLength(50)]
        public string city { get; set; }
        [StringLength(50)]
        public string country { get; set; }
        public int? postal_code { get; set; }
        [StringLength(100)]
        public string job_name { get; set; }
        public int? age { get; set; }
        public DateTime? created_at { get; set; }
        [StringLength(13)]
        [Unicode(false)]
        public string phone { get; set; }
        public bool? is_admin { get; set; }

        [InverseProperty("user")]
        public virtual ICollection<Cart> Carts { get; set; }
        [InverseProperty("user")]
        public virtual ICollection<Clients_say> Clients_says { get; set; }
        [InverseProperty("user")]
        public virtual ICollection<Contact> Contacts { get; set; }
        [InverseProperty("user")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}