﻿using GrandesRentACar.Models;

namespace GrandesRentACar.BusinessLogic
{
    public interface ICarData
    {
        Task<List<Car>> GetAllCars();
    }
}
