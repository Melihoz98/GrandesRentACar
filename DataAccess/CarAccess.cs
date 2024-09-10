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
        public async Task<List<CarCopy>> GetAvailableCarCopiesForCar(int carId)
        {
            var carCopies = new List<CarCopy>();
            string queryString = @"SELECT CarCopyID, LicenseNumber, CarID 
                           FROM CarsCopies 
                           WHERE CarID = @CarID"; // Modify the query if you have any additional conditions to check availability

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@CarID", carId);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var carCopy = new CarCopy
                                {
                                    CarCopyID = reader.GetInt32(0),
                                    LicenseNumber = reader.GetDecimal(1),
                                    CarID = reader.GetInt32(2)
                                };

                                carCopies.Add(carCopy);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching car copies: {ex.Message}");
            }

            return carCopies;
        }

        public async Task<Car> GetCarById(int carId)
        {
            Car car = null;
            string queryString = @"SELECT CarID, ModelName, ModelYear, FuelType, Description, GearType, DailyPrice, ImageUrl 
                                   FROM Cars 
                                   WHERE CarID = @CarID";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@CarID", carId);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                car = new Car
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
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching car details: {ex.Message}");
            }

            return car;
        }
    }
}
