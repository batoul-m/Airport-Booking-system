namespace BookingSystem{
    public class ManegerServices : IManagerServices{
        private readonly IBookingServices _bookingServices;
        public ManegerServices(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }
        public void FilterBookings(IBookingServices bookingServices)
        {
            Console.Write("Enter Flight ID (or leave empty): ");
            var flightID = Console.ReadLine();

            Console.Write("Enter Passenger ID (or leave empty): ");
            var passengerID = 0.0;
            var input = Console.ReadLine();
            double.TryParse(input, out passengerID);

            Console.Write("Enter Class Type (Economy, Business, FirstClass, or leave empty): ");
            var classType = Console.ReadLine();
            // Fetch bookings based on the criteria
            var bookings = bookingServices.GetBookingsByCriteria(flightID, passengerID, classType);
            // Display the bookings
            foreach (var booking in bookings)
                Console.WriteLine($"Booking ID: {booking.BookingID}, Flight ID: {booking.Flight.FlightID}, Passenger ID: {booking.Passenger.PassengerID}, Class: {booking.Class}, Date: {booking.BookingDate}");
            Console.ReadLine();
        }
        // Method to import flights from a CSV file
        public void ImportFlightsFromCsv(IFlightService flightService)
        {
            Console.Write("Enter CSV file path: ");
            var filePath = Console.ReadLine();
            flightService.ImportFlightsFromCsv(filePath);

            Console.WriteLine("Flights imported successfully.");
            Console.ReadLine();
        }
        // Method to view flight validation errors
        public void ViewValidationErrors(IFlightService flightService)
        {
            var errors = flightService.ValidateFlightData(new Flight("", "", "", "", "", DateTime.Now, 0, ""));
            if (errors.Count > 0){
                Console.WriteLine("Validation Errors:");
                foreach (var error in errors)
                    Console.WriteLine(error);
            }
            else
                Console.WriteLine("No validation errors found.");
            Console.ReadLine();
        }
    }
}