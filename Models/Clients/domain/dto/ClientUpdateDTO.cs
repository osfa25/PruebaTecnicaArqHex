namespace PruebaTecnicaArqHex.Models.Clients.domain.dto
{
    public class ClientUpdateDTO
    {
        public Guid Id { get; set; }
        public required string name { get; set; }
        public required string email { get; set; }
        public string? phone { get; set; }
    }
}
