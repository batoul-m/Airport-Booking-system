using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem{
    public class BookingServices
    {
        private List<Booking> bookings;

        public BookingServices()
        {
            bookings = new List<Booking>();
        }
        public Booking BookFlight(Passenger passenger, Flight flight, string classType)
        {
            string bookingId = Guid.NewGuid().ToString();

            Booking newBooking = new Booking(bookingId, classType, flight, DateTime.Now, passenger);

            bookings.Add(newBooking);

            return newBooking;
        }

        public void CancelBooking(string bookingId)
        {
            Booking booking = bookings.FirstOrDefault(b => b.BookingID == bookingId);

            if (booking != null)
            {
                booking.CancelBooking(); 
            }
            else
            {
                Console.WriteLine("Booking not found.");
            }
        }

        public void ModifyBooking(string bookingId, Flight newFlight, string newClass)
        {
            Booking booking = bookings.FirstOrDefault(b => b.BookingID == bookingId);

            if (booking != null)
            {
                booking.ModifyBooking(newClass, newFlight); 
            }
            else
            {
                Console.WriteLine("Booking not found.");
            }
        }

        public List<Booking> GetBookingsByPassenger(double passengerId)
        {
            return bookings.Where(b => b.Passenger.PassportNumber == passengerId).ToList();
        }

        public List<Booking> GetBookingsByCriteria(string flightId, double passengerId, string classType)
        {
            return bookings.Where(b =>
                (string.IsNullOrEmpty(flightId) || b.Flight.FlightID == flightId) &&
                (passengerId <= 0 || b.Passenger.PassportNumber == passengerId) &&
                (string.IsNullOrEmpty(classType) || b.Class == classType)
            ).ToList();
        }
    }
}
