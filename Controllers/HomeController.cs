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
            var availableCarCopies = await _carCopiesData.GetAvailableCars(startDate, endDate);

           
            var allCars = await _carData.GetAllCars();

           
            var carsWithAvailableCopies = new List<Car>();

          
            var availableCarIds = availableCarCopies.Select(cc => cc.CarID).Distinct();

           
            carsWithAvailableCopies = allCars.Where(car => availableCarIds.Contains(car.CarID)).ToList();

           
            return View("~/Views/AvailableCars/Index.cshtml", carsWithAvailableCopies); 
        }
        catch (Exception ex)
        {
            
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
