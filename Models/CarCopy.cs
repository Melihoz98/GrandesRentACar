namespace GrandesRentACar.Models
{
    public class CarCopy
    {


        CarCopy(int carCopyID, decimal licenseNumber, Car carID)
        {
            CarCopyID = carCopyID;
            LicenseNumber = licenseNumber;
            CarID = carID;

        }

        CarCopy(decimal licenseNumber, Car carID)
        {
            LicenseNumber = licenseNumber;
            CarID = carID;
        }

        CarCopy()
        {

        }


        public int CarCopyID { get; set; }
        public decimal LicenseNumber { get; set; }
        public Car CarID { get; set; }


    }
}
