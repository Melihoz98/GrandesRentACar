using GrandesRentACar.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace GrandesRentACar.DataAccess
{
    public class CarAccess : ICarAccess
    {
        // You may inject the connection string via constructor, or set it directly here
        private readonly string connectionString = "YourConnectionStringHere";

        public CarAccess()
        {
        }

        public async Task<List<Car>> GetAllCars()
        {
            var cars = new List<Car>();
            string queryString = @"SELECT CarID, ModelName, ModelYear, FuelType, Description, GearType, DailyPrice FROM Cars";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync(); // Open the connection

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync()) // Execute query
                        {
                            while (await reader.ReadAsync()) // Read each record
                            {
                                var car = new Car
                                {
                                    CarID = reader.GetInt32(0), // CarID
                                    ModelName = reader.GetString(1), // ModelName
                                    ModelYear = reader.GetDecimal(2), // ModelYear
                                    FuelType = reader.GetString(3), // FuelType
                                    Description = reader.IsDBNull(4) ? null : reader.GetString(4), // Check for NULL in Description
                                    GearType = reader.GetString(5), // GearType
                                    DailyPrice = reader.GetDecimal(6) // DailyPrice
                                };

                                cars.Add(car); // Add the car to the list
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception (based on your requirement)
                Console.WriteLine($"An error occurred while fetching cars: {ex.Message}");
            }

            return cars; // Return the list of cars
        }
    }
}
