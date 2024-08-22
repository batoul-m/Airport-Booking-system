using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookingSystem
{
    public class FileDataAccess
    {
        private string flightsFilePath = "flights.csv";
        private string bookingsFilePath = "bookings.csv";

        public List<Flight> LoadFlights()
        {
            if (!File.Exists(flightsFilePath))
            {
                throw new FileNotFoundException("Flights file not found.", flightsFilePath);
            }

            return CsvParser.ParseFlights(flightsFilePath);
        }

        public void SaveFlights(List<Flight> flights)
        {
            using (var writer = new StreamWriter(flightsFilePath))
            {
                writer.WriteLine("FlightID,DepartureCountry,DestinationCountry,DepartureDate,DepartureAirport,ArrivalAirport,Price,Class");
                foreach (var flight in flights)
                {
                    writer.WriteLine($"{flight.FlightID},{flight.DepartureCountry},{flight.DestinationCountry},{flight.DepartureDate:yyyy-MM-dd},{flight.DepartureAirport},{flight.ArrivalAirport},{flight.Price},{flight.Class}");
                }
            }
        }

        public List<Booking> LoadBookings()
        {
            if (!File.Exists(bookingsFilePath))
            {
                throw new FileNotFoundException("Bookings file not found.", bookingsFilePath);
            }

            return CsvParser.ParseBookings(bookingsFilePath);
        }

        public void SaveBookings(List<Booking> bookings)
        {
            using (var writer = new StreamWriter(bookingsFilePath))
            {
                writer.WriteLine("BookingID,Class,FlightID,BookingDate,PassengerID");
                foreach (var booking in bookings)
                {
                    writer.WriteLine($"{booking.BookingID},{booking.Class},{booking.Flight.FlightID},{booking.BookingDate:yyyy-MM-dd},{booking.Passenger.PassengerID}");
                }
            }
        }
    }
}
