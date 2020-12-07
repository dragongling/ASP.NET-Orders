using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Orders.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Количество")]
        public decimal Quantity { get; set; }

        [Display(Name = "Единица измерения")]
        public string Unit { get; set; }

        public virtual Order Order { get; set; }
    }
}