using System.Reflection;

namespace BookingSystem{
    public class Passenger
    {
        public string? Name{ get; set; }
        public string? PassengerID{ get; set; }
        public double? PassportNumber { get; set; }
        public Passenger(string name, string passengerID, double passportNumber)
        {
            Name = name.Length > 0? name : throw new ArgumentException("name can not be empty");
            PassengerID = passengerID.Length  >0 ? passengerID : throw new ArgumentException("passprit id must be not empty");
            PassportNumber = passportNumber > 0? passportNumber : throw new ArgumentException("passport number >0");
        }
    }
}