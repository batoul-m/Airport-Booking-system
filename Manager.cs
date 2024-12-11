using System;
using System.Collections.Generic;

namespace BookingSystem{

    public class Manager
    {
        private BookingServices bookingServices;
        private FlightService flightService;

        public Manager()
        {
            bookingServices = new BookingServices();
            flightService = new FlightService();
        }

        public void AddFlight(string flightID, string departureCountry, string destinationCountry, DateTime departureDate, string departureAirport, string arrivalAirport, decimal price, string flightClass)
        {
            var flight = new Flight(flightID, departureCountry, destinationCountry, departureDate, departureAirport, arrivalAirport, price, flightClass);
            flightService.AddFlight(flight);
        }

        public Booking BookFlight(Passenger passenger, string flightID, string classType)
        {
            var flight = flightService.SearchFlights(null, null, default, null, 0).Find(f => f.FlightID == flightID);
            if (flight == null)
                throw new ArgumentException("Flight not found.");

            return bookingServices.BookFlight(passenger, flight, classType);
        }

        public void CancelBooking(string bookingID)
        {
            bookingServices.CancelBooking(bookingID);
        }

        public void ModifyBooking(string bookingID, string newFlightID, string newClass)
        {
            var newFlight = flightService.SearchFlights(null, null, default, null, 0).Find(f => f.FlightID == newFlightID);
            if (newFlight == null)
                throw new ArgumentException("New flight not found.");

            bookingServices.ModifyBooking(bookingID, newFlight, newClass);
        }

        public void ImportFlightsFromCsv(string filePath)
        {
            flightService.ImportFlightsFromCsv(filePath);
        }

        public List<Booking> GetBookingsByPassenger(double passengerID)
        {
            return bookingServices.GetBookingsByPassenger(passengerID);
        }

        public List<Booking> GetBookingsByCriteria(string flightID, double passengerID, string classType)
        {
            return bookingServices.GetBookingsByCriteria(flightID, passengerID, classType);
        }
    }
}
