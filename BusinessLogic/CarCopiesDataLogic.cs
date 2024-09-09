using GrandesRentACar.Models;
using GrandesRentACar.DataAccess;

namespace GrandesRentACar.BusinessLogic
{
    public class CarCopiesDataLogic : ICarCopiesData
    {
        private readonly ICarCopiesAccess _carCopiesAccess;
        public CarCopiesDataLogic(ICarCopiesAccess carCopiesAccess) 
        {
        
            _carCopiesAccess = carCopiesAccess;
        
        }

        public async Task<List<CarCopy>> GetAvailableCars(DateTime startDate, DateTime endDate)
        {
            return await _carCopiesAccess.GetAvailableCars(startDate,  endDate);
        }

    }
}
