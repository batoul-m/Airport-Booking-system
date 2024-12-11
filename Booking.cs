using System;

namespace BookingSystem
{
    public class Booking
    {
        private string bookingID;
        private string bookingClass;
        private Flight flight;
        private DateTime bookingDate;
        private Passenger passenger; 


        public Booking()
        {
        }

        public Booking(string bookingID, string @class, Flight flight, DateTime bookingDate, Passenger passenger)
        {
            BookingID = bookingID;
            Class = @class;
            Flight = flight;
            BookingDate = bookingDate;
            Passenger = passenger;
        }

        public string BookingID
        {
            get { return bookingID; }
            set 
            { 
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("BookingID cannot be null or empty.");
                bookingID = value; 
            }
        }

        public string Class
        {
            get { return bookingClass; }
            set
            {
                if (value != "Economy" && value != "Business" && value != "First Class")
                {
                    throw new ArgumentException("Invalid class. Valid options are: Economy, Business, First Class.");
                }
                bookingClass = value;
            }
        }

        public Flight Flight
        {
            get { return flight; }
            set 
            { 
                flight = value ?? throw new ArgumentNullException(nameof(Flight), "Flight cannot be null."); 
            }
        }

        public DateTime BookingDate
        {
            get { return bookingDate; }
            set 
            { 
                if (value == default)
                    throw new ArgumentException("BookingDate cannot be default value.");
                bookingDate = value; 
            }
        }

        public Passenger Passenger
        {
            get { return passenger; }
            set 
            { 
                passenger = value ?? throw new ArgumentNullException(nameof(Passenger), "Passenger cannot be null."); 
            }
        }


        public void CancelBooking()
        {
            Console.WriteLine($"Booking {BookingID} has been canceled.");
        }

        public void ModifyBooking(string newClass, Flight newFlight)
        {
            Class = newClass;  
            Flight = newFlight ?? throw new ArgumentNullException(nameof(newFlight), "Flight cannot be null.");
            Console.WriteLine($"Booking {BookingID} has been modified.");
        }

        public void ViewBookingDetails()
        {
            Console.WriteLine($"Booking ID: {BookingID}");
            Console.WriteLine($"Passenger Name: {Passenger.Name}");
            Console.WriteLine($"Flight ID: {Flight.FlightID}");
            Console.WriteLine($"Class: {Class}");
            Console.WriteLine($"Booking Date: {BookingDate}");
            Console.WriteLine($"Departure: {Flight.DepartureCountry} -> {Flight.DestinationCountry}");
        }
    }
}