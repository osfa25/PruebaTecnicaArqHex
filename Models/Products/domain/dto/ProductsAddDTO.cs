namespace PruebaTecnicaArqHex.Models.Products.domain.dto
{
    public class ProductsAddDTO
    {
        public required string name { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
    }
}