using GrandesRentACar.BusinessLogic;
using GrandesRentACar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GrandesRentACar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ICarData _carData;
        

        public HomeController(ILogger<HomeController> logger, ICarData carData)
        {
            _logger = logger;
            _carData = carData;
        } 

        public async Task <IActionResult> Index()
        {
            List<Car> cars;
            cars = await _carData.GetAllCars();




            return View(cars);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
