using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GrandesRentACar.Models;
using GrandesRentACar.DataAccess;
using GrandesRentACar.BusinessLogic;
using System.Linq;
using Newtonsoft.Json;

namespace GrandesRentACar.Controllers
{
    public class SelectedCarController : Controller
    {
        private readonly ILogger<SelectedCarController> _logger;
        private readonly ICarData _carData;
        private const string ReservationSessionKey = "ReservationSession"; 


        public SelectedCarController(ILogger<SelectedCarController> logger, ICarData carData)
        {
            _logger = logger;
            _carData = carData;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int carId)
        {
            try
            {
                var car = await _carData.GetCarById(carId);

                if (car == null)
                {
                    return NotFound();
                }

                return View("Index", car); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the car details.");
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddCarToReservation(int carId)
        {
            // Clear existing CarCopyID in the session if it exists
            HttpContext.Session.Remove("CarCopyID");

            // Retrieve the reservation from the session
            var reservation = GetReservationFromSession();

            if (reservation == null)
            {
                // Log the error or handle it without returning a view
                _logger.LogWarning("No reservation found in session.");
                return StatusCode(400); // Bad request
            }

            // Fetch available car copies for the selected car
            var availableCarCopies = await _carData.GetAvailableCarCopiesForCar(carId); // Make sure _carData is an instance of ICarAccess

            if (availableCarCopies.Any())
            {
                var selectedCarCopy = availableCarCopies.First(); // For simplicity, take the first available car copy
                reservation.CarCopyID = selectedCarCopy; // Update reservation

                // Save the updated reservation back into the session
                SetReservationInSession(reservation);

                // No view or redirection; just complete the action
                return StatusCode(200); // OK
            }

            // Handle case where no available car copies are found
            _logger.LogWarning("No available car copies found for car ID: " + carId);
            return StatusCode(404); // Not found
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

        // Helper method to set the reservation in session
        private void SetReservationInSession(Reservation reservation)
        {
            string reservationJson = JsonConvert.SerializeObject(reservation);
            HttpContext.Session.SetString(ReservationSessionKey, reservationJson);
        }
    }
}
