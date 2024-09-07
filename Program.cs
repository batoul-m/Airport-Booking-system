using Microsoft.Extensions.DependencyInjection;
using BookingSystem;

class Program
{
    static void Main(string[] args)
    {
        // Setup Dependency Injection
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IFlightService, FlightService>()
            .AddSingleton<IBookingServices, BookingServices>()
            .AddSingleton<IFileDataAccess, FileDataAccess>()
            .AddSingleton<IManagerServices, ManagerServices>()
            .AddSingleton<IPassengerServices, PassengerServices>()
            .BuildServiceProvider();

        var flightService = serviceProvider.GetService<IFlightService>();
        var bookingServices = serviceProvider.GetService<IBookingServices>();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Airport Ticket Booking System");
            Console.WriteLine("1. Login as Passenger");
            Console.WriteLine("2. Login as Manager");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    HandlePassengerOperations(serviceProvider.GetService<IPassengerServices>(), flightService, bookingServices);
                    break;
                case "2":
                    HandleManagerOperations(serviceProvider.GetService<IManagerServices>(), flightService, bookingServices);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

        // Passenger Operations
        static void HandlePassengerOperations(FlightService flightService, BookingServices bookingServices)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Passenger Menu");
                Console.WriteLine("1. Book a Flight");
                Console.WriteLine("2. Search for Available Flights");
                Console.WriteLine("3. Cancel a Booking");
                Console.WriteLine("4. Modify a Booking");
                Console.WriteLine("5. View Personal Bookings");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BookFlight(bookingServices, flightService);
                        break;
                    case "2":
                        SearchFlights(flightService);
                        break;
                    case "3":
                        CancelBooking(bookingServices);
                        break;
                    case "4":
                        ModifyBooking(bookingServices, flightService);
                        break;
                    case "5":
                        ViewPersonalBookings(bookingServices);
                        break;
                    case "6":
                        return; // Back to Main Menu
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // Manager Operations
        static void HandleManagerOperations(FlightService flightService, BookingServices bookingServices)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Manager Menu");
                Console.WriteLine("1. Filter Bookings");
                Console.WriteLine("2. Import Flights from CSV");
                Console.WriteLine("3. View Validation Errors");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        FilterBookings(bookingServices);
                        break;
                    case "2":
                        ImportFlightsFromCsv(flightService);
                        break;
                    case "3":
                        ViewValidationErrors(flightService);
                        break;
                    case "4":
                        return; // Back to Main Menu
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // Methods for Passenger

        static void BookFlight(BookingServices bookingServices, FlightService flightService)
        {
            var passengerID = 0.0;
            var pass = 0.0;
            Console.Write("Enter Passenger ID: ");
            var input = Console.ReadLine();
            double.TryParse(input, out passengerID);
            Console.Write("Enter Passenger Name: ");
            var name = Console.ReadLine();
            Console.Write("Enter Passport Number ");
            input= Console.ReadLine();
            double.TryParse(input,out pass);

            var passenger = new Passenger(name,passengerID, pass );

            Console.Write("Enter Flight ID: ");
            var flightID = Console.ReadLine();
            Console.Write("Enter Class Type (Economy, Business, First Class): ");
            var classType = Console.ReadLine();

            var flight = flightService.SearchFlights(null, null, default, null, 0).Find(f => f.FlightID == flightID);
            if (flight == null)
            {
                Console.WriteLine("Flight not found.");
                Console.ReadLine();
                return;
            }

            var booking = bookingServices.BookFlight(passenger, flight, classType);

            Console.WriteLine($"Booking successful! Booking ID: {booking.BookingID}");
            Console.ReadLine();
        }

        static void SearchFlights(FlightService flightService)
        {
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
            {
                Console.WriteLine($"Flight ID: {flight.FlightID}, Departure: {flight.DepartureCountry} -> {flight.DestinationCountry}, Date: {flight.DepartureDate}, Price: {flight.Price}, Class: {flight.Class}");
            }

            Console.ReadLine();
        }

        static void CancelBooking(BookingServices bookingServices)
        {
            Console.Write("Enter Booking ID to cancel: ");
            var bookingID = Console.ReadLine();

            bookingServices.CancelBooking(bookingID);

            Console.WriteLine("Booking canceled successfully.");
            Console.ReadLine();
        }

        static void ModifyBooking(BookingServices bookingServices, FlightService flightService)
        {
            Console.Write("Enter Booking ID to modify: ");
            var bookingID = Console.ReadLine();

            Console.Write("Enter New Flight ID: ");
            var newFlightID = Console.ReadLine();
            Console.Write("Enter New Class Type (Economy, Business, First Class): ");
            var newClass = Console.ReadLine();

            var newFlight = flightService.SearchFlights(null, null, default, null, 0).Find(f => f.FlightID == newFlightID);
            if (newFlight == null)
            {
                Console.WriteLine("New flight not found.");
                Console.ReadLine();
                return;
            }

            bookingServices.ModifyBooking(bookingID, newFlight, newClass);

            Console.WriteLine("Booking modified successfully.");
            Console.ReadLine();
        }

        static void ViewPersonalBookings(BookingServices bookingServices)
        {
            var passengerID = 0.0;
            Console.Write("Enter Passenger ID: ");
            var  input = Console.ReadLine();
            double.TryParse(input,out passengerID);

            var bookings = bookingServices.GetBookingsByPassenger(passengerID);

            foreach (var booking in bookings)
            {
                Console.WriteLine($"Booking ID: {booking.BookingID}, Flight ID: {booking.Flight.FlightID}, Class: {booking.Class}, Date: {booking.BookingDate}");
            }

            Console.ReadLine();
        }

        // Methods for Manager

        static void FilterBookings(BookingServices bookingServices)
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
            {
                Console.WriteLine($"Booking ID: {booking.BookingID}, Flight ID: {booking.Flight.FlightID}, Passenger ID: {booking.Passenger.PassengerID}, Class: {booking.Class}, Date: {booking.BookingDate}");
            }

            Console.ReadLine();
        }

        static void ImportFlightsFromCsv(FlightService flightService)
        {
            Console.Write("Enter CSV file path: ");
            var filePath = Console.ReadLine();

            flightService.ImportFlightsFromCsv(filePath);

            Console.WriteLine("Flights imported successfully.");
            Console.ReadLine();
        }

        static void ViewValidationErrors(FlightService flightService)
        {
            // Assuming ValidateFlightData returns errors, otherwise adapt this method
            var errors = flightService.ValidateFlightData(new Flight("", "", "", DateTime.Now, "", "", 0, ""));
            if (errors.Count > 0)
            {
                Console.WriteLine("Validation Errors:");
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
            }
            else
            {
                Console.WriteLine("No validation errors found.");
            }

            Console.ReadLine();
        }
    }

