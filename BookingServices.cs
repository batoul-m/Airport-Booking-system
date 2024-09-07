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
        }

        public List<Booking> GetBookingsByCriteria(string flightId, double passengerId, string classType)
        {
            return _bookings.Where(b =>
                (string.IsNullOrEmpty(flightId) || b.Flight.FlightID == flightId) &&
                (passengerId <= 0 || b.Passenger.PassportNumber == passengerId)
            ).ToList();
        }
    }
}
