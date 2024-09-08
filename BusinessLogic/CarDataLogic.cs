using GrandesRentACar.DataAccess;
using GrandesRentACar.Models;

namespace GrandesRentACar.BusinessLogic
{
    public class CarDataLogic : ICarData
    {
        private readonly ICarAccess _carAccess;
    public CarDataLogic (ICarAccess carAccess)
    {
        _carAccess = carAccess;
    }


        public async Task<List<Car>> GetAllCars()
        {
            return await _carAccess.GetAllCars();
        }

    }

    


}
