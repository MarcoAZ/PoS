using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoS.Models
{
    public class Discount
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(25)]
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only Alpha numeric allowed.")]
        [Remote("IsNameExist", "Discount", AdditionalFields= "Id", ErrorMessage = "Name already in use")]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Range(0, 100.00, ErrorMessage = "Must be a number 0 - 100.00")]
        public decimal ? FixedAmount { get; set; }

        [Range(1, 100, ErrorMessage = "Must be an integer 1 - 100")]
        public int ? PercentAmount { get; set; }
    }

}