using GrandesRentACar.Models;

namespace GrandesRentACar.BusinessLogic
{
    public interface ICarData
    {
        Task<List<Car>> GetAllCars();
        Task<Car> GetCarById(int carId);
        Task<List<CarCopy>> GetAvailableCarCopiesForCar(int carId);

    }
}
