namespace GrandesRentACar.Models
{
    public class Customer
    {

        public Customer(int customerID, string firstName, string lastName, string address, string city, string email)
        {
            CustomerID = customerID;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            Email = email;
        }

        public Customer(string firstName, string lastName, string address, string city, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            Email = email;
        }
        public Customer() { }


        public int CustomerID { get; set; } 
        public string FirstName { get; set; }   
        public string LastName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }


    }
}
