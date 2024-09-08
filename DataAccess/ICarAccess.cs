using GrandesRentACar.Models;

namespace GrandesRentACar.DataAccess
{
    public interface ICarAccess
    {
        Task<List<Car>> GetAllCars();
    }
}
