namespace PruebaTecnicaArqHex.Models.Invoices.domain.dto
{
    public class InvoiceAddDTO
    {
        public Guid ClientID { get; set; }
        public decimal Total { get; set; }
    }
}