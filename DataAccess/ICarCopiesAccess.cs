using GrandesRentACar.Models;

namespace GrandesRentACar.DataAccess
{
    public interface ICarCopiesAccess
    {

        Task<List<CarCopy>> GetAvailableCars(DateTime startDate, DateTime endDate);
    }
}
