namespace PruebaTecnicaArqHex.Models.Products.domain.dto
{
    public class ProductsUpdateDTO
    {
        public required string name { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
    }
}