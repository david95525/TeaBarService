using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeaBarService.Models
{
    public class Discount
    {
        [Key]
        public int DiscountID { get; set; }
        public double DiscountRule { get; set; }
        public string Description { get; set; }

        public List<Orders> Orders { get; set; }
    }
}