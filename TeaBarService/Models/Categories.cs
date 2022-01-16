﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaBarService.Models
{
    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }

        [ForeignKey("Stores")]
        public string StoreID { get; set; }

        public string CategoryName { get; set; }

        public virtual Stores Stores { get; set; }
        public virtual IList<Products> Products { get; set; }
    }
}