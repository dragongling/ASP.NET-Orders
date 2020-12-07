using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Orders.Models
{
    public class Provider
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Поставщик")]
        public string Name { get; set; }
    }
}