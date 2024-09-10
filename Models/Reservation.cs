namespace GrandesRentACar.Models
{
    public class Reservation
    {

        public Reservation(int reservationID, DateTime startDate, DateTime endDate, CarCopy carCopyID, Customer customerID, Car carID)
        {
            ReservationID = reservationID;
            StartDate = startDate;
            EndDate = endDate;
            CarCopyID = carCopyID;
            CustomerID = customerID;
            CarID = carID;

        }

        public Reservation(DateTime startDate, DateTime endDate, CarCopy carCopyID, Customer customerID, Car carID)
        {
            StartDate = startDate;
            EndDate = endDate;
            CarCopyID = carCopyID;
            CustomerID = customerID;
            CarID = carID;
            

        }

        public Reservation() { }


        public int ReservationID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CarCopy CarCopyID { get; set; }
        public Customer CustomerID { get; set; }
        public Car CarID { get; set; }

    }
}
