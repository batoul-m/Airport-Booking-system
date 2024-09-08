using Microsoft.Extensions.DependencyInjection;
using BookingSystem;
class Program{
    static void Main(string[] args)
    {
        // Setup dependency injection
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IPassengerServices, PassengerServices>()
            .AddSingleton<IManagerServices, ManegerServices>()
            .AddSingleton<IBookingServices, BookingServices>()
            .AddSingleton<IFlightService, FlightService>()
            .AddSingleton<IFileDataAccess, FileDataAccess>()
            .AddSingleton<ICsvParser,CsvParser>()
            .BuildServiceProvider();

        var passengerServices = serviceProvider.GetRequiredService<IPassengerServices>();
        var managerServices = serviceProvider.GetRequiredService<IManagerServices>();
        var bookingServices = serviceProvider.GetRequiredService<IBookingServices>();
        var flightService = serviceProvider.GetRequiredService<IFlightService>();

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
                    HandlePassengerOperations(passengerServices, bookingServices, flightService);
                    break;
                case "2":
                    HandleManagerOperations(managerServices, bookingServices, flightService);
                    break;
                case "3":
                    return; // Exit the application
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    // Passenger Operations
    public static void HandlePassengerOperations(IPassengerServices passengerServices, IBookingServices bookingServices, IFlightService flightService)
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
                    passengerServices.BookFlightByFlightId(bookingServices, flightService);
                    break;
                case "2":
                    passengerServices.SearchFlights(flightService);
                    break;
                case "3":
                    passengerServices.CancelBookingById(bookingServices);
                    break;
                case "4":
                    passengerServices.ModifyBooking(bookingServices, flightService);
                    break;
                case "5":
                    passengerServices.ViewPersonalBookings(bookingServices);
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
    public static void HandleManagerOperations(IManagerServices managerServices, IBookingServices bookingServices, IFlightService flightService)
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
                    managerServices.FilterBookings(bookingServices);
                    break;
                case "2":
                    managerServices.ImportFlightsFromCsv(flightService);
                    break;
                case "3":
                    managerServices.ViewValidationErrors(flightService);
                    break;
                case "4":
                    return; // Back to Main Menu
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
