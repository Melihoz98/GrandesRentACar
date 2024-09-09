namespace GrandesRentACar.Models
{
    public class CarCopy
    {


        public CarCopy(int carCopyID, decimal licenseNumber, Car carID)
        {
            CarCopyID = carCopyID;
            LicenseNumber = licenseNumber;
            CarID = carID;

        }

        public CarCopy(decimal licenseNumber, Car carID)
        {
            LicenseNumber = licenseNumber;
            CarID = carID;
        }

        public CarCopy()
        {

        }


        public int CarCopyID { get; set; }
        public decimal LicenseNumber { get; set; }
        public Car CarID { get; set; }


    }
}
