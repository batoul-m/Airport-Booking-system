using System;

namespace BookingSystem
{
    public enum FlightClass{
        Economy,
        Business,
        FirstClass }
    public class Flight{
        public string FlightID{ get; set; }
        public string DepartureCountry{ get; set; }
        public string DestinationCountry{ get; set; }
        public DateTime DepartureDate{ get; set; }
        public string DepartureAirport{ get; set; }
        public string ArrivalAirport{ get; set; }
        public decimal Price{ get; set; }
        public FlightClass Class{ get; set; }

        public Flight(string flightID,string departureAirport,string departureCountry,string destinationCountry,string arrivalAirport,DateTime departureDate,decimal price,string classFlight){
            FlightID = string.IsNullOrEmpty(flightID)
                    ?flightID : throw new ArgumentNullException("can not be null or empty");
            DepartureCountry = string.IsNullOrEmpty(departureCountry)
                    ?departureCountry: throw new ArgumentNullException("can not be null or empty");           
            DestinationCountry = string.IsNullOrEmpty(destinationCountry)
                    ?destinationCountry: throw new ArgumentNullException("can not be null or empty");
            DepartureAirport = string.IsNullOrEmpty(departureAirport)
                    ?departureAirport: throw new ArgumentNullException("can not be null or empty");
            ArrivalAirport = string.IsNullOrEmpty(arrivalAirport)
                    ?arrivalAirport: throw new ArgumentNullException("can not be null or empty");
            DepartureDate = departureDate > DateTime.Now ? departureDate : throw new ArgumentException("can not take past dates");
            Price = price > 0 ? price : 0;
            if (Enum.TryParse(classFlight, out FlightClass flightClass) && Enum.IsDefined(typeof(FlightClass), flightClass))
                Class = flightClass; // Assign the parsed enum value
            else throw new ArgumentException("Class not valid", nameof(classFlight));
        }
    }
}
