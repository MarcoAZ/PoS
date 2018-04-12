using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace PoS.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only letters allowed.")]
        public string ServerFirst { get; set; }
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only letters allowed.")]
        public string ServerLast { get; set; }

        public float Subtotal { get; set; }
        public float DiscountAmount { get; set; }
        public float PreTaxTotal { get; set; }
        public float Tax { get; set; }
        public float Total { get; set; }

    }

    public class OrderDetailsContext : DbContext
    {
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}