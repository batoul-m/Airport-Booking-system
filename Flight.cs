using System;

namespace BookingSystem
{
    public class Flight
    {
        private string flightID;
        private string departureCountry;
        private string destinationCountry;
        private DateTime departureDate;
        private string departureAirport;
        private string arrivalAirport;
        private decimal price;
        private string flightClass;

        public Flight()
        {
        }

        public Flight(string flightID, string departureCountry, string destinationCountry, DateTime departureDate, string departureAirport, string arrivalAirport, decimal price, string flightClass)
        {
            FlightID = flightID;
            DepartureCountry = departureCountry;
            DestinationCountry = destinationCountry;
            DepartureDate = departureDate;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            Price = price;
            Class = flightClass;
        }

        public string FlightID
        {
            get { return flightID; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("FlightID cannot be null or empty.");
                flightID = value;
            }
        }

        public string DepartureCountry
        {
            get { return departureCountry; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Departure Country cannot be null or empty.");
                departureCountry = value;
            }
        }

        public string DestinationCountry
        {
            get { return destinationCountry; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Destination Country cannot be null or empty.");
                destinationCountry = value;
            }
        }

        public DateTime DepartureDate
        {
            get { return departureDate; }
            set
            {
                if (value < DateTime.Now)
                    throw new ArgumentException("Departure date must be in the future.");
                departureDate = value;
            }
        }

        public string DepartureAirport
        {
            get { return departureAirport; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Departure Airport cannot be null or empty.");
                departureAirport = value;
            }
        }

        public string ArrivalAirport
        {
            get { return arrivalAirport; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Arrival Airport cannot be null or empty.");
                arrivalAirport = value;
            }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Price must be greater than zero.");
                price = value;
            }
        }

        public string Class
        {
            get { return flightClass; }
            set
            {
                if (value != "Economy" && value != "Business" && value != "First Class")
                {
                    throw new ArgumentException("Invalid class. Valid options are: Economy, Business, First Class.");
                }
                flightClass = value;
            }
        }

        public bool IsAvailable(DateTime date, string departureCountry, string destinationCountry)
        {
            return this.DepartureDate.Date == date.Date &&
                   string.Equals(this.DepartureCountry, departureCountry, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(this.DestinationCountry, destinationCountry, StringComparison.OrdinalIgnoreCase);
        }
    }
}
