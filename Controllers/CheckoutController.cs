using GrandesRentACar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrandesRentACar.Controllers
{
    public class CheckoutController : Controller
    {
        private const string ReservationSessionKey = "ReservationSession";
        private readonly IConfiguration _configuration;

        public CheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var checkoutDetails = GetReservationFromSession();
            return View(checkoutDetails);
        }

        private Reservation GetReservationFromSession()
        {
            var reservationJson = HttpContext.Session.GetString(ReservationSessionKey);
            if (string.IsNullOrEmpty(reservationJson))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Reservation>(reservationJson);
        }

        [HttpPost]
        public IActionResult Pay(decimal totalPrice)
        {
            // PayPal API credentials from appsettings.json
            var clientId = _configuration["PayPal:ClientId"];
            var clientSecret = _configuration["PayPal:ClientSecret"];

            var apiContext = new APIContext(new OAuthTokenCredential(clientId, clientSecret).GetAccessToken());

            // Create a payment request
            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer
                {
                    payment_method = "paypal"
                },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = "Reservation Payment",
                        invoice_number = "YOUR_INVOICE_NUMBER",
                        amount = new Amount
                        {
                            currency = "USD",
                            total = totalPrice.ToString()
                        }
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    cancel_url = Url.Action("Cancel", "Checkout", null, Request.Scheme),
                    return_url = Url.Action("Success", "Checkout", null, Request.Scheme)
                }
            };

            var createdPayment = payment.Create(apiContext);

            // Redirect to PayPal for approval
            var approvalUrl = createdPayment.links.FirstOrDefault(x => x.rel == "approval_url")?.href;
            return Redirect(approvalUrl);
        }

        public IActionResult Success(string paymentId, string token, string PayerID)
        {
            var clientId = _configuration["PayPal:ClientId"];
            var clientSecret = _configuration["PayPal:ClientSecret"];

            var apiContext = new APIContext(new OAuthTokenCredential(clientId, clientSecret).GetAccessToken());

            var paymentExecution = new PaymentExecution { payer_id = PayerID };
            var payment = new Payment { id = paymentId };

            var executedPayment = payment.Execute(apiContext, paymentExecution);

            // Handle payment success
            return View("PaymentSuccess");
        }

        public IActionResult Cancel()
        {
            // Handle payment cancellation
            return View("PaymentCancelled");
        }
    }
}
