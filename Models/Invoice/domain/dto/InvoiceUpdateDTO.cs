namespace PruebaTecnicaArqHex.Models.Invoices.domain.dto
{
    public class InvoiceUpdateDTO
    {
        public Guid ClientID { get; set; }
        public decimal Total { get; set; }
    }
}