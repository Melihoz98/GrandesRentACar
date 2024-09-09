using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrandesRentACar.Models;
using GrandesRentACar.DataAccess;
using GrandesRentACar.BusinessLogic;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICarData _carData;
    private readonly ICarCopiesData _carCopiesData;

    public HomeController(ILogger<HomeController> logger, ICarData carData, ICarCopiesData carCopiesData)
    {
        _logger = logger;
        _carData = carData;
        _carCopiesData = carCopiesData;
    }

    public async Task<IActionResult> Index()
    {
        List<Car> cars = await _carData.GetAllCars();
        return View(cars);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> AvailableCars(DateTime startDate, DateTime endDate)
    {
        // Fetch available cars based on the selected dates
        var availableCars = await _carCopiesData.GetAvailableCars(startDate, endDate);

        // Check if availableCars has data
        if (availableCars == null || !availableCars.Any())
        {
            // Optional: Log or handle the case where no cars are available
            // ViewBag.Message = "No cars available for the selected dates.";
        }

        return View("Index", availableCars); // Use "Index" to match the view file name
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
