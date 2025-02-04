using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaArqHex.Data;
using PruebaTecnicaArqHex.Models.Invoices.domain.entities;
using PruebaTecnicaShared;


[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public InvoiceController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllInvoices()
        {
            var allInvoices = dbContext.Invoices.ToList();
            return Ok(allInvoices);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetInvoiceById(Guid id)
        {
            var invoice = dbContext.Invoices.Find(id);

            if (invoice is null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        [HttpPost]
        public IActionResult AddInvoice(InvoiceDTO invoiceAddDTO)
        {
            var client = dbContext.Clients.Find(invoiceAddDTO.ClientID);

            if (client is null)
            {
                return NotFound();
            }

            var invoiceEntity = new Invoices()
            {
                Client = client,
                Total = invoiceAddDTO.Total,
            };

            dbContext.Invoices.Add(invoiceEntity);
            dbContext.SaveChanges();
            return Ok(invoiceEntity);

        }

    [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateInvoice(Guid id, InvoiceDTO invoiceUpdateDTO)
        {
            var invoice = dbContext.Invoices.Find(id);

            if (invoice is null)
            {
                return NotFound();
            }

            invoice.ClientID = invoiceUpdateDTO.ClientID;
            invoice.Total = invoiceUpdateDTO.Total;

            dbContext.SaveChanges();
            return Ok(invoice);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteInvoice(Guid id)
        {
            var invoice = dbContext.Invoices.Find(id);

            if (invoice is null)
            {
                return NotFound();
            }

            dbContext.Invoices.Remove(invoice);
            dbContext.SaveChanges();
            return Ok();
        }
    }
