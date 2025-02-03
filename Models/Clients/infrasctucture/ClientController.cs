using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaArqHex.Data;
using PruebaTecnicaArqHex.Models.Clients.domain.dto;
using PruebaTecnicaArqHex.Models.Clients.domain.entities;

namespace PruebaTecnicaArqHex.Models.Clients.infrasctucture
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ClientController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllClients()
        {
            var allClients = dbContext.Clients.ToList();

            return Ok(allClients);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetClientById(Guid id)
        {
           var client =  dbContext.Clients.Find(id);

            if (client is null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public IActionResult AddClient(ClientAddDTO clientAddDTO)
        {
            var clientEntity = new Client()
            {
                name = clientAddDTO.name,
                email = clientAddDTO.email,
                phone = clientAddDTO.phone
            };
            dbContext.Clients.Add(clientEntity);
            dbContext.SaveChanges();
            return Ok(clientEntity);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateClient(Guid id, ClientUpdateDTO clientUpdateDTO)
        {
            var client = dbContext.Clients.Find(id);

            if(client is null)
            {
                return NotFound();
            }

            client.name = clientUpdateDTO.name;
            client.email = clientUpdateDTO.email;
            client.phone = clientUpdateDTO.phone;

            dbContext.SaveChanges();
            return Ok(client);
        }

        [HttpDelete]
        public IActionResult DeleteClient(Guid id)
        {
            var client = dbContext.Clients.Find(id);

            if(client is null)
            {
                return NotFound();
            }

            dbContext.Clients.Remove(client);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
