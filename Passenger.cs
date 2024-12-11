using System.Reflection;

namespace BookingSystem{
    public class Passenger
    {
        private string name ;
        private double passengerID;
        private double passportNumber ;

        public Passenger(string name, double passengerID, double passportNumber)
        {
            Name = name;
            PassengerID = passengerID;
            PassportNumber = passportNumber;
        }

        public Passenger() { }

        public string Name{
            get{return name; }
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new ArgumentException("Passenger name can not be null or empty");
                name = value;
            }

        }

        public double PassengerID{
            get{return passengerID; }
            set
            {
                if(value <= 0)
                    throw new ArgumentException("Passenger ID can not be 0 or neg");
                passengerID = value;
            }

        }

        public double PassportNumber{
            get{return passportNumber; }
            set
            {
                if( value <= 0)
                    throw new ArgumentException("Passport Number can not be 0");
                passportNumber = value; 
            }
        }
    }
}