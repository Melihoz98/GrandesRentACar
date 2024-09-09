using GrandesRentACar.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GrandesRentACar.DataAccess
{
    public class CarAccess : ICarAccess
    {
        private readonly string _connectionString;

        public CarAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Database connection string is not configured.");
        }

        public async Task<List<Car>> GetAllCars()
        {
            var cars = new List<Car>();
            string queryString = @"SELECT CarID, ModelName, ModelYear, FuelType, Description, GearType, DailyPrice, ImageUrl FROM Cars";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var car = new Car
                                {
                                    CarID = reader.GetInt32(0),
                                    ModelName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                    ModelYear = reader.IsDBNull(2) ? (decimal?)null : reader.GetDecimal(2),
                                    FuelType = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Description = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    GearType = reader.IsDBNull(5) ? null : reader.GetString(5),
                                    DailyPrice = reader.IsDBNull(6) ? (decimal?)null : reader.GetDecimal(6),
                                    ImageUrl = reader.IsDBNull(7) ? null : reader.GetString(7)
                                };

                                cars.Add(car);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching cars: {ex.Message}");
            }

            return cars;
        }
    }
}
