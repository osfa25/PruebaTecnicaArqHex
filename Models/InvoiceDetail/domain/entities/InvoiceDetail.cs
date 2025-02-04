using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PruebaTecnicaArqHex.Models.Invoices.domain.entities;
using PruebaTecnicaArqHex.Models.Products.domain.entities;

    public class InvoiceDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Required]
        public Guid InvoiceID { get; set; }

        [Required]
        public Guid ProductID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be positive.")]
        public decimal UnitPrice { get; set; }

        [NotMapped]
        public decimal Subtotal => Quantity * UnitPrice;

        [ForeignKey("InvoiceID")]
        public required Invoices Invoice { get; set; }

        [ForeignKey("ProductID")]
        public required Products Product { get; set; }
    }

