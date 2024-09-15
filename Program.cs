using System;
using BookingSystem;

    class Program
    {   
        static void Main(string[] args)
        {
            var flightService = new FlightService();
            
            var bookingServices = new BookingServices();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Airport Ticket Booking System");
                Console.WriteLine("1. Login as Passenger");
                Console.WriteLine("2. Login as Manager");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice){
                    case "1":
                        HandlePassengerOperations(flightService, bookingServices);
                        break;
                    case "2":
                        HandleManagerOperations(flightService, bookingServices);
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
        public static void HandlePassengerOperations(FlightService flightService, BookingServices bookingServices)
        {
            var _passengerServices = new PassengerServices();
            while (true){
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
                switch (choice){
                    case "1":
                        _passengerServices.BookFlightByFlightId(bookingServices,flightService);
                        break;
                    case "2":
                        _passengerServices.SearchFlights(flightService);
                        break;
                    case "3":
                        _passengerServices.CancelBookingById(bookingServices);
                        break;
                    case "4":
                        _passengerServices.ModifyBooking(bookingServices,flightService);
                        break;
                    case "5":
                        _passengerServices.ModifyBooking(bookingServices,flightService);
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
        public static void HandleManagerOperations(FlightService flightService, BookingServices bookingServices)
        {   var _manegerServices = new ManegerServices();
            while (true){
                Console.Clear();
                Console.WriteLine("Manager Menu");
                Console.WriteLine("1. Filter Bookings");
                Console.WriteLine("2. Import Flights from CSV");
                Console.WriteLine("3. View Validation Errors");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();
                switch (choice){
                    case "1":
                        _manegerServices.FilterBookings(bookingServices);
                        break;
                    case "2":
                        _manegerServices.ImportFlightsFromCsv(flightService);
                        break;
                    case "3":
                        _manegerServices.ViewValidationErrors(flightService);
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

