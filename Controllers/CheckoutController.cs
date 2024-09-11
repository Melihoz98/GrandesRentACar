using GrandesRentACar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;

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
            // Payment processing logic to be added here if needed (after removing PayPal)
            // Currently just redirecting to a success page as placeholder
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
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
