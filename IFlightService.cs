namespace BookingSystem
{
    public interface IFlightService
    {
        List<Flight> SearchFlights(string departureCountry, string destinationCountry, DateTime departureDate, string classType, decimal maxPrice);
        Flight? SearchFlightsById(string flightID);
        void AddFlight(Flight flight);
        void ImportFlightsFromCsv(string filePath);
        List<string> ValidateFlightData(Flight flight);
    }
}