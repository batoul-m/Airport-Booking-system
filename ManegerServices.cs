using System;
namespace BookingSystem{
    public class ManegerServices{
        private List<Booking> _bookings;
        public ManegerServices(){
            _bookings = new List<Booking>();
        }
        public List<Booking> GetBookingsByCriteria(string flightId, double passengerId, string classType){
            return _bookings.Where(b =>
                (string.IsNullOrEmpty(flightId) || b.Flight.FlightID == flightId) &&
                (passengerId <= 0 || b.Passenger.PassportNumber == passengerId) &&
                (string.IsNullOrEmpty(classType) )
            ).ToList();
        }
        public void FilterBookings(BookingServices bookingServices)
        {
            Console.Write("Enter Flight ID (or leave empty): ");
            var flightID = Console.ReadLine();
            Console.Write("Enter Passenger ID (or leave empty): ");
            var passengerID  = 0.0;
            var input= Console.ReadLine();
            double.TryParse(input,out passengerID );
            Console.Write("Enter Class Type (Economy, Business, First Class, or leave empty): ");
            var classType = Console.ReadLine();
            var bookings = bookingServices.GetBookingsByCriteria(flightID, passengerID, classType);

            foreach (var booking in bookings)
                Console.WriteLine($"Booking ID: {booking.BookingID}, Flight ID: {booking.Flight.FlightID}, Passenger ID: {booking.Passenger.PassengerID}, Class: {booking.Class}, Date: {booking.BookingDate}");
            Console.ReadLine();
        }

        public void ImportFlightsFromCsv(FlightService flightService)
        {
            Console.Write("Enter CSV file path: ");
            var filePath = Console.ReadLine();
            flightService.ImportFlightsFromCsv(filePath);
            Console.WriteLine("Flights imported successfully.");
            Console.ReadLine();
        }
        public void ViewValidationErrors(FlightService flightService)
        {
            var errors = flightService.ValidateFlightData(new Flight("", "", "", "", "", DateTime.Now, 0, ""));
            if (errors.Count > 0)
            {
                Console.WriteLine("Validation Errors:");
                foreach (var error in errors)
                    Console.WriteLine(error);  
            }
            else Console.WriteLine("No validation errors found.");
            Console.ReadLine();
        }
    }
}