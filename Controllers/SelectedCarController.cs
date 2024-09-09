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
    }
}
