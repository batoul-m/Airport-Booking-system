namespace BookingSystem
{
    public interface IPassengerServices{
        void BookFlightByFlightId(IBookingServices bookingServices, IFlightService flightService);
        void SearchFlights(IFlightService flightService);
        void CancelBookingById(IBookingServices bookingServices);
        void ModifyBooking(IBookingServices bookingServices, IFlightService flightService);
        void ViewPersonalBookings(IBookingServices bookingServices);
    }
}