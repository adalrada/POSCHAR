using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace POSCHAR.Models
{
    public class Vendor
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Código")]
        public Guid Code { get; set; }
        [DisplayName("Nombre")]
        public string Name { get; set; }
        [DisplayName("Dirección")]
        public string Address { get; set; }
        [DisplayName("número de telefono")]
        public string PhoneNumber { get; set; }
        [EmailAddress, DisplayName("Correo")]
        public string Email { get; set; }
    }
}
