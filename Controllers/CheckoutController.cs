using GrandesRentACar.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GrandesRentACar.Controllers
{
    public class CheckoutController : Controller
    {
        private const string ReservationSessionKey = "ReservationSession";

        public IActionResult Index()
        {
            var checkoutDetails = GetReservationFromSession();
            return View(checkoutDetails);
        }

        // Helper method to get the reservation from session
        private Reservation GetReservationFromSession()
        {
            var reservationJson = HttpContext.Session.GetString(ReservationSessionKey);
            if (string.IsNullOrEmpty(reservationJson))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<Reservation>(reservationJson);
        }

    }
}
