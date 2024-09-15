namespace BookingSystem
{
    public interface IFileDataAccess{
        List<Flight> LoadFlights(string flightsFilePath);
        void SaveFlights(List<Flight> flights);
        List<Booking> LoadBookings(string bookingFilePath);
        void SaveBookings(List<Booking> bookings);
    }
}