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
            HttpContext.Session.Remove("CarID");
            var reservation = GetReservationFromSession();

            if (reservation != null)
            {
                try
                {
                    var car = await _carData.GetCarById(carId);

                    if (car == null)
                    {
                        return NotFound();
                    }

                    reservation.CarID = car; // Assign the full Car object
                    SetReservationInSession(reservation); // Save the updated reservation in session

                    return View("Index", car);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while fetching the car details.");
                    return View("Error");
                }
            }
            else
            {
                // Handle the case where there is no reservation in the session
                _logger.LogWarning("No reservation found in session.");
                return RedirectToAction("Error"); // Redirect to an error page or handle accordingly
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

        [HttpPost]
        public IActionResult BookCar(string FirstName, string LastName, string Address, string City, string Email)
        {
            HttpContext.Session.Remove("FirstName");
            HttpContext.Session.Remove("LastName");
            HttpContext.Session.Remove("Address");
            HttpContext.Session.Remove("City");
            HttpContext.Session.Remove("Email");
            // Retrieve the reservation from the session
            var reservation = GetReservationFromSession();


            var customer = new Customer
            {
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                City = City,
                Email = Email
            };
            reservation.CustomerID = customer;

            // Store the updated reservation in the session
            SetReservationInSession(reservation);

            // Redirect or respond with success after storing the reservation
            return RedirectToAction("Index", "Checkout");
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
