using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orders.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Номер")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        [Display(Name = "Поставщик")]
        public int ProviderId { get; set; }

        public virtual Provider Provider { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}