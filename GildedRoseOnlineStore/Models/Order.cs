using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseOnlineStore.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        [StringLength(100)]
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime PlaceOrderDate { get; set; }
        [Required]
        public string State { get; set; }


    }
}


       

