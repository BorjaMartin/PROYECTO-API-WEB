using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NeoCheck.FraudPrevention.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FraudPreventionController : ControllerBase
    {

        private readonly ILogger<FraudPreventionController> _logger;

        public FraudPreventionController(ILogger<FraudPreventionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PurchaseInformation> Get()
        {
            PurchaseInformation purchase = new PurchaseInformation();
            purchase.OrderId = 1;
            purchase.DealId = 1;
            purchase.EmailAddress = "bugs @bunny.com";
            purchase.StreetAddress = "123 Sesame St.";
            purchase.City = "New York";
            purchase.State = "NY";
            purchase.ZipCode = "10011";
            purchase.CreditCardNumber = 12345689010;


            List<PurchaseInformation> ListPurchase = new List<PurchaseInformation>();
            ListPurchase.Add(purchase);

            PurchaseInformation[] resultpurchase = ListPurchase.ToArray();
            return resultpurchase;
        }

        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> Validate([FromBody] PurchaseInformationRequest purchases)
        {
            //TODO: Your code here!!
            return BadRequest();

        }
    }
}
