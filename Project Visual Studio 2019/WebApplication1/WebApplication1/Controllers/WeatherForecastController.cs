using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        /*
        public PurchaseInformation ValidEmail(PurchaseInformation valuePurchase, List<PurchaseInformation> purchasesConfirm)
        {
            PurchaseInformation validPurchase = new PurchaseInformation();

            IEnumerable<PurchaseInformation> purchaseQuery = from PurchaseInformation in purchasesConfirm
                                                             where PurchaseInformation.EmailAddress == valuePurchase.EmailAddress
                                                             && PurchaseInformation.OrderId == PurchaseInformation.OrderId
                                                             select PurchaseInformation;

            if (purchaseQuery.Count() > 0)
            {
                //Ya existe una compra con misma dirección de correo e Id de compra
                return null;
            }
            else
            {
                return validPurchase;
            }


        }

        */


        public WeatherForecastController(ILogger<WeatherForecastController> logger)
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

        [Route("2")]
        [HttpGet]
        public IEnumerable<PurchaseInformation> Get2()
        {
            PurchaseInformation purchase = new PurchaseInformation();
            purchase.OrderId = 2;
            purchase.DealId = 2;
            purchase.EmailAddress = "bugs2 @bunny.com";
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

        [Route("validate/{purchases}")]
        [HttpPost]
        public async Task<IActionResult> Validate([FromBody] PurchaseInformationRequest purchases)
        {
            //TODO: Your code here!!
            //Listaa con las compras que llegan por parametro
            List<PurchaseInformation> ReqPurchases = purchases.Purchases;
            
            //Lista con las compras que van cumpliendo las validaciones y son validas.
            List<PurchaseInformation> ConfirmPurchases = new List<PurchaseInformation>();

            //Lista de respuesta ocn os identificadores de compra fraudulentos
            List<String> returnFraudulent = new List<String>();



            if (ReqPurchases.Count() > 0)
            {
                //Me recorro las compras que me llegan por parametro JSON
                foreach (PurchaseInformation item in ReqPurchases)
                {

                    //Nos creamos una variable auxiliar con la dirección de correo desde el @
                    int num = item.EmailAddress.ToUpper().IndexOf("@");
                    int num2 = item.EmailAddress.ToUpper().Length;
                    String dircorreo = item.EmailAddress.ToUpper().ToString().Substring(item.EmailAddress.ToUpper().IndexOf("@"), item.EmailAddress.ToUpper().Length - num);

                    //Ignormamos los . de la dirección de correo.
                    String aux = item.EmailAddress.ToUpper().Replace(".", "");

                    string auxEmail2 = item.EmailAddress.ToString();

                    //Si el correo tiene un + ignoramos el simbolo + y todo lo que va despues hasta el @
                    if (item.EmailAddress.IndexOf("+") != -1)
                    {
                        auxEmail2 = item.EmailAddress.ToString().ToUpper().Replace(".", "").Substring(0, item.EmailAddress.IndexOf("+")-1);
                        auxEmail2 = auxEmail2 + dircorreo;
                    }

                    //Comprobacón de dos pedidos con misma dirección de correo e ID de oferta
                    IEnumerable<PurchaseInformation> purchaseQuery = from PurchaseInformation in ConfirmPurchases
                                                                     where PurchaseInformation.EmailAddress.ToUpper().Replace(".","") == auxEmail2.ToUpper().Replace(".", "")
                                                                     && PurchaseInformation.DealId == item.DealId
                                                                     && PurchaseInformation.CreditCardNumber != item.CreditCardNumber
                                                                     select PurchaseInformation;


                    if (purchaseQuery.Count() > 0)
                    {
                        //Este pedio es fraudulento
                        //Existen dos pedidos con la misma dirección de correo electrónico e ID de oferta, pero diferente información de tarjeta de crédito, independientemente de la dirección.

                        foreach (PurchaseInformation item2 in purchaseQuery)
                        {
                            
                            if (!returnFraudulent.Contains(item2.OrderId.ToString()))
                            {
                                returnFraudulent.Add(item2.OrderId.ToString());
                                
                            }  

                        }

                        if (!returnFraudulent.Contains(item.OrderId.ToString()))                        
                            returnFraudulent.Add(item.OrderId.ToString());
                        

                    }
                    else
                    {
                        //Comprobacón de dos pedidos con misma dirección address , ciudad , estado y CP e ID de oferta
                        //Pero diferente numero de tarjeta
                        IEnumerable<PurchaseInformation> purchaseQuery2 = from PurchaseInformation in ConfirmPurchases
                                                                          where
                                                                          (PurchaseInformation.StreetAddress.ToUpper() == item.StreetAddress.ToUpper()
                                                                            || PurchaseInformation.StreetAddress.ToUpper() == (item.StreetAddress.Contains("Street") ? item.StreetAddress.Replace("Street", "St.").ToUpper() : item.StreetAddress.Replace("St.", "Street").ToUpper())
                                                                            || PurchaseInformation.StreetAddress.ToUpper() == (item.StreetAddress.Contains("Road") ? item.StreetAddress.Replace("Road", "Rd.").ToUpper() : item.StreetAddress.Replace("Rd.", "Road").ToUpper())
                                                                            )
                                                                          && PurchaseInformation.City == item.City
                                                                          //Validaciones de Estado
                                                                          //IL -- ILLINOIS, CA -- CALIFORNIA, NY - New York
                                                                          && (PurchaseInformation.State.ToUpper() == item.State.ToUpper()
                                                                            || PurchaseInformation.State.ToUpper() == (item.State.ToUpper().Contains("NEW YORK") ? item.State.ToUpper().Replace("NEW YORK", "NY").ToUpper() : item.State.ToUpper().Replace("NY", "NEW YORK").ToUpper())
                                                                            || PurchaseInformation.State.ToUpper() == (item.State.ToUpper().Contains("CALIFORNIA") ? item.State.ToUpper().Replace("CALIFORNIA", "CA").ToUpper() : item.State.ToUpper().Replace("CA", "CALIFORNIA").ToUpper())
                                                                            || PurchaseInformation.State.ToUpper() == (item.State.ToUpper().Contains("ILLINOIS") ? item.State.ToUpper().Replace("ILLINOIS", "IL").ToUpper() : item.State.ToUpper().Replace("IL", "ILLINOIS").ToUpper())
                                                                            )

                                                                          && PurchaseInformation.ZipCode == item.ZipCode
                                                                          && PurchaseInformation.CreditCardNumber != item.CreditCardNumber
                                                                          select PurchaseInformation;


                        if (purchaseQuery2.Count() > 0)
                        {
                            //Este pedio es fraudulento, 
                            //Existen dos pedidos que tienen la misma dirección / ciudad / estado / código postal e identificación de la oferta, 
                            //pero diferente información de la tarjeta de crédito, independientemente de la dirección de correo electrónico

                            foreach (PurchaseInformation item3 in purchaseQuery2)
                            {

                                if (!returnFraudulent.Contains(item3.OrderId.ToString()))
                                {
                                    returnFraudulent.Add(item3.OrderId.ToString());

                                }

                            }

                            if (!returnFraudulent.Contains(item.OrderId.ToString()))
                                returnFraudulent.Add(item.OrderId.ToString());


                        }
                        else
                        {
                            ConfirmPurchases.Add(item);
                        }

                        
                    }

                }

                //return Ok(JsonConvert.SerializeObject(ConfirmPurchases, Formatting.Indented));
                return Ok(JsonConvert.SerializeObject(returnFraudulent, Formatting.Indented));
            }
            else
            {
                return BadRequest("Error parametros de entrada");
            }

        }


    }
}
