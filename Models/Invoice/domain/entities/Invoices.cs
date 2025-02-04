using PruebaTecnicaArqHex.Models.Clients.domain.entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaArqHex.Models.Invoices.domain.entities
{
    public class Invoices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Required]
        public Guid ClientID { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total must be a positive value.")]
        public decimal Total { get; set; }

        [ForeignKey("ClientID")]
        public required Client Client { get; set; }
    }
}