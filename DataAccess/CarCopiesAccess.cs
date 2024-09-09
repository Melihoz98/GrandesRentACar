using GrandesRentACar.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GrandesRentACar.DataAccess
{
    public class CarCopiesAccess : ICarCopiesAccess
    {
        private readonly string _connectionString;

        public CarCopiesAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Database connection string is not configured.");
        }

        public async Task<List<CarCopy>> GetAvailableCars(DateTime startDate, DateTime endDate)
        {
            var availableCars = new List<CarCopy>();
            string queryString = @"
        SELECT CarCopyID, LicenseNumber, CarID
        FROM CarsCopies
        WHERE CarCopyID NOT IN (
            SELECT CarCopyID
            FROM Reservations
            WHERE (@StartDate <= EndDate AND @EndDate >= StartDate)
        )";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Date);
                        command.Parameters.AddWithValue("@EndDate", endDate.Date);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var carCopy = new CarCopy
                                {
                                    CarCopyID = reader.GetInt32(0),
                                    LicenseNumber = reader.GetDecimal(1),
                                    CarID = new Car { CarID = reader.GetInt32(2) }
                                };

                                availableCars.Add(carCopy);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching available cars: {ex.Message}");
            }

            return availableCars;
        }

    }
}
