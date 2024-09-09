namespace GrandesRentACar.Models
{
    public class CarCopy
    {
        public CarCopy(int carCopyID, decimal licenseNumber, int carID)
        {
            CarCopyID = carCopyID;
            LicenseNumber = licenseNumber;
            CarID = carID;
        }

        public CarCopy(decimal licenseNumber, int carID)
        {
            LicenseNumber = licenseNumber;
            CarID = carID;
        }

        public CarCopy() { }

        public int CarCopyID { get; set; }
        public decimal LicenseNumber { get; set; }
        public int CarID { get; set; } // This should be an int
    }
}
