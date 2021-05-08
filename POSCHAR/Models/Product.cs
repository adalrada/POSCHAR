﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace POSCHAR.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Código")]
        public Guid Code { get; set; }
        [DisplayName("Tipo")]
        public string Type { get; set; }
        [DisplayName("Nombre")]
        public string Name { get; set; }
        [DisplayName("Precio")]
        public decimal Price { get; set; }
        [DisplayName("Precio de compra")]
        public decimal CostPrice { get; set; }
        [DisplayName("Cantidad")]
        public int Quantity { get; set; }
        [DisplayName("Estado")]
        public string Status { get; set; }

    }
}
