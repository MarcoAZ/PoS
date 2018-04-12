using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoS.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Must be a four digit number.")]
        public int UserName { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(16)]
        public string Password { get; set; }

        [Required]
        [MinLength(1)]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only letters allowed.")]
        public string LastName { get; set; }

        [Required]
        [MinLength(1)]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only letters allowed.")]
        public string FirstName { get; set; }

    }

    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }

}