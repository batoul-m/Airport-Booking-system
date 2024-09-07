namespace BookingSystem
{
<<<<<<< HEAD
    public class FlightService : IFlightService{
        private readonly IFileDataAccess _dataAccess;
        private List<Flight> _flights;
        public FlightService(IFileDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
=======
    public class FlightService
    {
        private List<Flight> _flights;
        private FileDataAccess _dataAccess;
        public FlightService()
        {
>>>>>>> projectUsingPattern
            _flights = new List<Flight>();
        }
        public List<Flight> SearchFlights(string departureCountry, string destinationCountry, DateTime departureDate, string classType, decimal maxPrice)
        {
            return _flights.Where(f =>
                (string.IsNullOrEmpty(departureCountry) || f.DepartureCountry.Equals(departureCountry, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(destinationCountry) || f.DestinationCountry.Equals(destinationCountry, StringComparison.OrdinalIgnoreCase)) &&
                (departureDate == default || f.DepartureDate.Date == departureDate.Date) &&
                (maxPrice <= 0 || f.Price <= maxPrice)
            ).ToList();
        }
<<<<<<< HEAD
        public Flight? SearchFlightsById(string flightID)
        {
            return _flights.FirstOrDefault(f => f.FlightID == flightID);
=======
        public Flight? SearchFlightsById(string flightID){
            if (string.IsNullOrEmpty(flightID)) return null;
            return _flights.FirstOrDefault(f => f.FlightID == flightID);   
>>>>>>> projectUsingPattern
        }
        public void AddFlight(Flight flight)
        {
            if (flight is null)
                throw new ArgumentNullException(nameof(flight), "Flight cannot be null.");
            var validationErrors = ValidateFlightData(flight);
            if (validationErrors.Any())
                throw new InvalidOperationException("Cannot add flight. Validation errors: " + string.Join(", ", validationErrors));
            _flights.Add(flight);
        }
        public void ImportFlightsFromCsv(string filePath)
        {
<<<<<<< HEAD
            _flights = _dataAccess.LoadFlights(filePath);
        }
=======
            _dataAccess.LoadFlights(filePath);
        }
            
>>>>>>> projectUsingPattern
        public List<string> ValidateFlightData(Flight flight)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(flight.FlightID))
                errors.Add("FlightID is required.");
            if (string.IsNullOrEmpty(flight.DepartureCountry))
                errors.Add("DepartureCountry is required.");
            if (string.IsNullOrEmpty(flight.DestinationCountry))
                errors.Add("DestinationCountry is required.");
            if (flight.DepartureDate == default)
                errors.Add("DepartureDate is required.");
            if (string.IsNullOrEmpty(flight.DepartureAirport))
                errors.Add("DepartureAirport is required.");
            if (string.IsNullOrEmpty(flight.ArrivalAirport))
                errors.Add("ArrivalAirport is required.");
            if (flight.Price <= 0)
                errors.Add("Price must be greater than zero.");
            return errors;
        }
    }
}
