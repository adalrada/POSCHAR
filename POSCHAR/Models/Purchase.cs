using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace POSCHAR.Models
{
    public class Purchase
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Código")]
        public Guid Code { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("Fecha")]
        public DateTimeOffset? SaleOrderDate { get; set; } = DateTime.Now;
        [DisplayName("Estado")]
        public string Stauts { get; set; }

        //Foreigns Keys
        [DisplayName("Provedor")]
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        [DisplayName("Producto")]
        public virtual List<PurchaseOrderLine> PurchaseOrderLine { get; set; } = new List<PurchaseOrderLine>();
    }
    public class PurchaseOrderLine
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Código")]
        public Guid PurchaseOrderLineCode { get; set; }
        [DisplayName("Número de factura")]
        public string InvoiceNumber { get; set; }
        [DisplayName("Catidad")]
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
        [DisplayName("Orden de compra")]
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        [DisplayName("Producto")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
