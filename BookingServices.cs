<<<<<<< HEAD
namespace BookingSystem
{
    public class BookingServices : IBookingServices{
        private readonly IFileDataAccess _dataAccess;
        private List<Booking> _bookings;
        public BookingServices(IFileDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _bookings = new List<Booking>();
        }
        public Booking BookFlight(Passenger passenger, Flight flight, string classType)
        {
            var booking = new Booking();  // Assume Booking class is properly defined
            _bookings.Add(booking);
            _dataAccess.SaveBookings(_bookings);
            return booking;
        }
        public void CancelBooking(string bookingID)
        {
            var booking = _bookings.FirstOrDefault(b => b.BookingID == bookingID);
            if (booking is not null)
            {
                _bookings.Remove(booking);
                _dataAccess.SaveBookings(_bookings);
            }
        }
        public void ModifyBooking(string bookingID, Flight newFlight, string newClass)
        {
            var booking = _bookings.FirstOrDefault(b => b.BookingID == bookingID);
            if (booking is not null)
            {
                booking.Flight = newFlight;
                booking.Class = newClass;
                _dataAccess.SaveBookings(_bookings);
            }
        }

        public List<Booking> GetBookingsByPassenger(double passengerID)
        {
            return _bookings.Where(b => b.Passenger.PassportNumber == passengerID).ToList();
=======
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem{
    public class BookingServices{   
        private List<Booking> _bookings;
        public BookingServices(){
            _bookings = new List<Booking>();
        }
        public Booking BookFlight(Passenger passenger, Flight flight, string classType)
        {   string bookingId = Guid.NewGuid().ToString();
            Booking newBooking = new Booking(bookingId, classType, flight, DateTime.Now, passenger);
            _bookings.Add(newBooking);
            return newBooking;
        }
        public void CancelBooking(string bookingId)
        {
            Booking? booking = _bookings.FirstOrDefault(b => b.BookingID == bookingId);
            if (booking != null) Console.WriteLine($"Booking {booking.BookingID} has been canceled.");
            else Console.WriteLine("Booking not found."); 
        }
        public void ModifyBooking(string bookingId, Flight newFlight, string newClass)
        {
            Booking? booking = _bookings.FirstOrDefault(b => b.BookingID == bookingId);
            if (booking != null){
                if (Enum.TryParse(newClass, out BookingClass bookingClass) && Enum.IsDefined(typeof(BookingClass), bookingClass))
                    booking.Class = bookingClass;                  
                else{
                    Console.WriteLine("Invalid booking class.");
                    return;}
                booking.Flight = newFlight ?? throw new ArgumentNullException(nameof(newFlight), "Flight cannot be null.");
                Console.WriteLine($"Booking {booking.BookingID} has been modified."); 
            }
            else Console.WriteLine("Booking not found.");
            
        }

        public List<Booking> GetBookingsByPassenger(double passengerId){
            return _bookings.Where(b => b.Passenger.PassportNumber == passengerId).ToList();
>>>>>>> projectUsingPattern
        }

        public List<Booking> GetBookingsByCriteria(string flightId, double passengerId, string classType)
        {
            return _bookings.Where(b =>
                (string.IsNullOrEmpty(flightId) || b.Flight.FlightID == flightId) &&
<<<<<<< HEAD
                (passengerId <= 0 || b.Passenger.PassportNumber == passengerId)
=======
                (passengerId <= 0 || b.Passenger.PassportNumber == passengerId) &&
                (string.IsNullOrEmpty(classType) )
>>>>>>> projectUsingPattern
            ).ToList();
        }
    }
}
