namespace GrandesRentACar.Models
{
    public class Car
    {


        public Car(int carID, string modelName, decimal modelYear, string fuelType, string description, string gearType, decimal dailyPrice)
        {
            CarID = carID;
            ModelName = modelName;
            ModelYear = modelYear;
            FuelType = fuelType;
            Description = description;
            GearType = gearType;
            DailyPrice = dailyPrice;

        }
        public Car(string modelName, decimal modelYear, string fuelType, string description, string gearType, decimal dailyPrice)
        {
            ModelName = modelName;
            ModelYear = modelYear;
            FuelType = fuelType;
            Description = description;
            GearType = gearType;
            DailyPrice = dailyPrice;

        }
        public Car()
        {
        }


        public int CarID { get; set; }
        public string ModelName { get; set; }
        public decimal ModelYear { get; set; }
        public string FuelType { get; set; }
        public string Description { get; set; }
        public string GearType { get; set; }
        public decimal DailyPrice { get; set; }



    }
}
