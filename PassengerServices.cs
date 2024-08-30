using System;
namespace BookingSystem{
    public class PassengerServices{
        public PassengerServices(){}
        //Booking Flight by entering the flight id
        public void BookFlightByFlightId(BookingServices bookingServices, FlightService flightService)
        {
            var pass = 0.0;
            Console.Write("Enter Passenger ID: ");
            var passengerID = Console.ReadLine();
            Console.Write("Enter Passenger Name: ");
            var name = Console.ReadLine();
            Console.Write("Enter Passport Number ");
            var input= Console.ReadLine();
            double.TryParse(input,out pass);
            var passenger = new Passenger(name,passengerID, pass );
            Console.Write("Enter Flight ID: ");
            var flightID = Console.ReadLine();
            Console.Write("Enter Class Type (Economy, Business, First Class): ");
            var classType = Console.ReadLine();
            var flight = flightService.SearchFlightsById(flightID);
            if (flight is null){
                Console.WriteLine("Flight not found.");
                Console.ReadLine();
                return;
            }
            var booking = bookingServices.BookFlight(passenger, flight, classType);
            Console.WriteLine($"Booking successful! Booking ID: {booking.BookingID}");
            Console.ReadLine();
        }
        //Search Flight by entering it informatiom
        public void SearchFlights(FlightService flightService){
            Console.Write("Enter Departure Country: ");
            var departureCountry = Console.ReadLine();
            Console.Write("Enter Destination Country: ");
            var destinationCountry = Console.ReadLine();
            Console.Write("Enter Departure Date (yyyy-MM-dd): ");
            var departureDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter Class Type (Economy, Business, First Class): ");
            var classType = Console.ReadLine();
            Console.Write("Enter Max Price: ");
            var maxPrice = decimal.Parse(Console.ReadLine());
            var flights = flightService.SearchFlights(departureCountry, destinationCountry, departureDate, classType, maxPrice);
            foreach (var flight in flights)
                Console.WriteLine($"Flight ID: {flight.FlightID}, Departure: {flight.DepartureCountry} -> {flight.DestinationCountry}, Date: {flight.DepartureDate}, Price: {flight.Price}, Class: {flight.Class}");
            Console.ReadLine();
        }
        //Cancel passenger booking by the booking ID
        public void CancelBookingById(BookingServices bookingServices)
        {
            Console.Write("Enter Booking ID to cancel: ");
            var bookingID = Console.ReadLine();

            bookingServices.CancelBooking(bookingID);

            Console.WriteLine("Booking canceled successfully.");
            Console.ReadLine();
        }
        public void ModifyBooking(BookingServices bookingServices, FlightService flightService)
        {
            Console.Write("Enter Booking ID to modify: ");
            var bookingID = Console.ReadLine();
            Console.Write("Enter New Flight ID: ");
            var newFlightID = Console.ReadLine();
            Console.Write("Enter New Class Type (Economy, Business, First Class): ");
            var newClass = Console.ReadLine();
            var newFlight = flightService.SearchFlightsById(newFlightID);
            if (newFlight == null){Console.WriteLine("New flight not found.");
                Console.ReadLine();
                return;
            }
            bookingServices.ModifyBooking(bookingID, newFlight, newClass);
            Console.WriteLine("Booking modified successfully.");
            Console.ReadLine();
        }
        public void ViewPersonalBookings(BookingServices bookingServices)
        {
            var passengerID = 0.0;
            Console.Write("Enter Passenger ID: ");
            var  input = Console.ReadLine();
            double.TryParse(input,out passengerID);
            var bookings = bookingServices.GetBookingsByPassenger(passengerID);
            foreach (var booking in bookings)
                Console.WriteLine($"Booking ID: {booking.BookingID}, Flight ID: {booking.Flight.FlightID}, Class: {booking.Class}, Date: {booking.BookingDate}");
            Console.ReadLine();
        }

    }
}
