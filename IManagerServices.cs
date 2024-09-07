namespace BookingSystem
{
    public interface IManagerServices{
        void FilterBookings(IBookingServices bookingServices);
        void ImportFlightsFromCsv(IFlightService flightService);
        void ViewValidationErrors(IFlightService flightService);
    }
}