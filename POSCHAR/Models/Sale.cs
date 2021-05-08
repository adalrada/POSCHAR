using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace POSCHAR.Models
{
    public class Sale
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Código")]
        public Guid Code { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("Fecha")]
        public DateTimeOffset? SaleOrderDate { get; set; } = DateTime.Now;
        [DisplayName("Fecha de entrega")]
        public DateTimeOffset? SaleDeliveryDate { get; set; }
        [DisplayName("estado")]
        public string Stauts { get; set; }
        //Foreigns Keys
        [DisplayName("Cliente")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [DisplayName("Linea de venta")]
        public virtual List<SalesOrderLine> SalesOrderLine { get; set; } = new List<SalesOrderLine>();
	}

    public class SalesOrderLine
    {
        public int Id { get; set; }
        [DisplayName("Código")]
        public Guid SalesOrderLineCode { get; set; }
        [DisplayName("Cantidad")]
        public int Quantity { get; set; }
        [DisplayName("Precio")]
        public decimal Price { get; set; }
        [DisplayName("Subtotal")]
        public decimal SubTotal { get; set; }
        [DisplayName("Descuento")]
        public decimal Discount { get; set; }
        [DisplayName("Iva")]
        public decimal Iva { get; set; }
        [DisplayName("Total")]
        public decimal Total { get; set; }

        //Foreigns Keys
        [DisplayName("Orden de venta")]
        public int SalesOrderId { get; set; }
        public Sale SalesOrder { get; set; }
        [DisplayName("Producto")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
