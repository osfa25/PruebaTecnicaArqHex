using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaArqHex.Data;
using PruebaTecnicaShared;


[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public InvoiceDetailController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllInvoiceDetails()
        {
            var allInvoiceDetails = dbContext.InvoiceDetails.ToList();
            return Ok(allInvoiceDetails);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetInvoiceDetailById(Guid id)
        {
            var invoiceDetail = dbContext.InvoiceDetails.Find(id);

            if (invoiceDetail is null)
            {
                return NotFound();
            }

            return Ok(invoiceDetail);
        }

    [HttpPost]
    public IActionResult AddInvoiceDetail(InvoiceDetailDTO invoiceDetailAddDTO)
    {
        
        var invoice = dbContext.Invoices.Find(invoiceDetailAddDTO.InvoiceID);
        var product = dbContext.Products.Find(invoiceDetailAddDTO.ProductID);

        if (invoice == null || product == null)
        {
            return NotFound();
        }

        var invoiceDetailEntity = new InvoiceDetail()
        {
            Invoice = invoice,
            Product = product,
            Quantity = invoiceDetailAddDTO.Quantity,
            UnitPrice = invoiceDetailAddDTO.UnitPrice
        };

        dbContext.InvoiceDetails.Add(invoiceDetailEntity);
        dbContext.SaveChanges();
        return Ok(invoiceDetailEntity);
    }


    [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateInvoiceDetail(Guid id, InvoiceDetailDTO invoiceDetailUpdateDTO)
        {
            var invoiceDetail = dbContext.InvoiceDetails.Find(id);

            if (invoiceDetail is null)
            {
                return NotFound();
            }

            invoiceDetail.InvoiceID = invoiceDetailUpdateDTO.InvoiceID;
            invoiceDetail.ProductID = invoiceDetailUpdateDTO.ProductID;
            invoiceDetail.Quantity = invoiceDetailUpdateDTO.Quantity;
            invoiceDetail.UnitPrice = invoiceDetailUpdateDTO.UnitPrice;

            dbContext.SaveChanges();
            return Ok(invoiceDetail);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteInvoiceDetail(Guid id)
        {
            var invoiceDetail = dbContext.InvoiceDetails.Find(id);

            if (invoiceDetail is null)
            {
                return NotFound();
            }

            dbContext.InvoiceDetails.Remove(invoiceDetail);
            dbContext.SaveChanges();
            return Ok();
        }
    }
