using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrandesRentACar.Models;
using GrandesRentACar.DataAccess;
using GrandesRentACar.BusinessLogic;
using System.Diagnostics;
using System.Linq;

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
        try
        {
            // Fetch available car copies based on the selected dates
            var availableCarCopies = await _carCopiesData.GetAvailableCars(startDate, endDate);

            // Fetch all cars to get their details
            var allCars = await _carData.GetAllCars();

            // Create a list to hold the cars with available copies
            var carsWithAvailableCopies = new List<Car>();

            // Get a list of CarIDs that have available copies
            var availableCarIds = availableCarCopies.Select(cc => cc.CarID).Distinct();

            // Filter all cars to include only those with available copies
            carsWithAvailableCopies = allCars.Where(car => availableCarIds.Contains(car.CarID)).ToList();

            // Pass the list of cars with available copies to the view in AvailableCars folder
            return View("~/Views/AvailableCars/Index.cshtml", carsWithAvailableCopies); // Specify the full path to the view file
        }
        catch (Exception ex)
        {
            // Log the exception and show an error page or message
            _logger.LogError(ex, "An error occurred while fetching available cars.");
            return View("Error");
        }
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
