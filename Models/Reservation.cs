namespace GrandesRentACar.Models
{
    public class Reservation
    {

        public Reservation(int reservationID, DateTime startDate, DateTime endDate, CarCopy carCopyID, Customer customerID)
        {
            ReservationID = reservationID;
            StartDate = startDate;
            EndDate = endDate;
            CarCopyID = carCopyID;
            CustomerID = customerID;

        }

        public Reservation(DateTime startDate, DateTime endDate, CarCopy carCopyID, Customer customerID)
        {
            StartDate = startDate;
            EndDate = endDate;
            CarCopyID = carCopyID;
            CustomerID = customerID;

        }

        public Reservation() { }


        public int ReservationID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CarCopy CarCopyID { get; set; }
        public Customer CustomerID { get; set; }

    }
}
