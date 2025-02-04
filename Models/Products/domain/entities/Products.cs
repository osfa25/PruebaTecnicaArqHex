using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaArqHex.Models.Products.domain.entities
{
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        [Required]
        public required string name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser positivo.")]
        public decimal price { get; set; }
        public int stock { get; set; }
    }
}
