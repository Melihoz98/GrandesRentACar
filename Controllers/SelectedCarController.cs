using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GrandesRentACar.Models;
using GrandesRentACar.DataAccess;
using GrandesRentACar.BusinessLogic;
using System.Linq;

namespace GrandesRentACar.Controllers
{
    public class SelectedCarController : Controller
    {
        private readonly ILogger<SelectedCarController> _logger;
        private readonly ICarData _carData;

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
                // Fetch the car details based on the CarID
                var car = await _carData.GetCarById(carId);

                // Check if the car was found
                if (car == null)
                {
                    // Optionally log or handle the case where the car is not found
                    return NotFound();
                }

                // Pass the car details to the view
                return View("Index", car); // Ensure "Index" matches your view file
            }
            catch (Exception ex)
            {
                // Log the exception and show an error page or message
                _logger.LogError(ex, "An error occurred while fetching the car details.");
                return View("Error");
            }
        }
    }
}
