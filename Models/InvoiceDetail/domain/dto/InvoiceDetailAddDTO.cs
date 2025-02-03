namespace PruebaTecnicaArqHex.Models.InvoiceDetail.domain.dto
{
    public class InvoiceDetailAddDTO
    {
        public Guid InvoiceID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}