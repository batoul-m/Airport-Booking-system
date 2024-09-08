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
                if (Enum.TryParse<BookingClass>(newClass, true, out var parsedClass) && Enum.IsDefined(typeof(BookingClass), parsedClass))
                    booking.Class = parsedClass;
                else
                    Console.WriteLine($"Invalid class type: {newClass}. Please enter a valid class (Economy, Business, FirstClass).");
               _dataAccess.SaveBookings(_bookings);
            }
        }

        public List<Booking> GetBookingsByPassenger(double passengerID)
        {
            return _bookings.Where(b => b.Passenger.PassportNumber == passengerID).ToList();
        }

        public List<Booking> GetBookingsByCriteria(string flightId, string passengerId, string classType)
        {
            return _bookings.Where(b =>
                (string.IsNullOrEmpty(flightId) || b.Flight.FlightID == flightId) &&
                (string.IsNullOrEmpty(passengerId) || b.Passenger.PassportNumber <= 0)
            ).ToList();
        }
    }

    
}
