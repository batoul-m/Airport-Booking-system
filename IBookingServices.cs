namespace BookingSystem
{
    public interface IBookingServices{
        Booking BookFlight(Passenger passenger, Flight flight, string classType);
        void CancelBooking(string bookingID);
        void ModifyBooking(string bookingID, Flight newFlight, string newClass);
        List<Booking> GetBookingsByPassenger(double passengerID);
        List<Booking> GetBookingsByCriteria(string flightId, double passengerId, string classType);
    }
}