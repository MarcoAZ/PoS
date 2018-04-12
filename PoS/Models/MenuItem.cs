using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoS.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(25)]
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only Alpha numeric allowed.")]
        [Remote("IsItemNameExist", "MenuItem", AdditionalFields ="Id", ErrorMessage = "Name already in use")]
        public string Name {get;set;}

        [Required]
        [Range(1, 100.00, ErrorMessage= "Must be an decimal 1 - 100")]
        public decimal Cost {get;set;}
    }

    public class MenuContext : DbContext
    {
        public DbSet<MenuItem> MenuItems { get; set; }
    }
}