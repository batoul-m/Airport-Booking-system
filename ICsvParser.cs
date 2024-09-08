namespace BookingSystem
{
    public interface ICsvParser{
        public List<Flight> ParseFlights(string filePath);
        public List<Booking> ParseBookings(string filePath);
        
    }
}