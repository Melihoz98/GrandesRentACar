using GrandesRentACar.Models;

namespace GrandesRentACar.DataAccess
{
    public interface ICarAccess
    {
        Task<List<Car>> GetAllCars();
        Task<Car> GetCarById(int carId);
        Task<List<CarCopy>> GetAvailableCarCopiesForCar(int carId);

    }
}
