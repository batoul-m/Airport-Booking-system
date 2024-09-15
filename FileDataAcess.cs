using System;
namespace BookingSystem{
    public class FileDataAccess{    
        private string _flightsFilePath = "flights.csv";
        private string _bookingsFilePath = "bookings.csv";
        private CsvParser? _csvParser;
        public List<Flight> LoadFlights(string flightsFilePath)
        {   _flightsFilePath = flightsFilePath;
            if (!File.Exists(_flightsFilePath))
                throw new FileNotFoundException("Flights file not found.", _flightsFilePath);
            var lines = File.ReadAllLines(_flightsFilePath);
            if (lines.Length <= 1) 
                throw new InvalidDataException("CSV file is empty or missing data.");
            var header = lines[0].Split(',');
            if (header.Length < 8) 
                throw new InvalidDataException("CSV file format is invalid.");
            return _csvParser.ParseFlights(_flightsFilePath);
        }

        public void SaveFlights(List<Flight> flights)
        {
            using (var writer = new StreamWriter(_flightsFilePath))
            {
                writer.WriteLine("FlightID,DepartureCountry,DestinationCountry,DepartureDate,DepartureAirport,ArrivalAirport,Price,Class");
                foreach (var flight in flights)
                    writer.WriteLine($"{flight.FlightID},{flight.DepartureCountry},{flight.DestinationCountry},{flight.DepartureDate:yyyy-MM-dd},{flight.DepartureAirport},{flight.ArrivalAirport},{flight.Price},{flight.Class}");  
            }
        }

        public List<Booking> LoadBookings()
        {
            if (!File.Exists(_bookingsFilePath))
                throw new FileNotFoundException("Bookings file not found.", _bookingsFilePath);
            return _csvParser.ParseBookings(_bookingsFilePath);
        }
        public void SaveBookings(List<Booking> bookings)
        {
            using (var writer = new StreamWriter(_bookingsFilePath)){
                writer.WriteLine("BookingID,Class,FlightID,BookingDate,PassengerID");
                foreach (var booking in bookings)
                    writer.WriteLine($"{booking.BookingID},{booking.Class},{booking.Flight.FlightID},{booking.BookingDate:yyyy-MM-dd},{booking.Passenger.PassengerID}");
                
            }
        }
    }
}
