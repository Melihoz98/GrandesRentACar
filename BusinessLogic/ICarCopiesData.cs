using GrandesRentACar.Models;

namespace GrandesRentACar.BusinessLogic
{
    public interface ICarCopiesData
    {
        Task<List<CarCopy>> GetAvailableCars(DateTime startDate, DateTime endDate);

    }
}
