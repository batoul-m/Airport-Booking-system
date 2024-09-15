using System;

namespace BookingSystem
{
    public enum BookingClass{
        Economy,
        Business,
        FirstClass }
    public class Booking {
        public string? BookingID{ get; set; }
        public BookingClass? Class{ get; set; }
        public Flight Flight{ get; set; }
        public DateTime BookingDate{ get; set; }
        public Passenger Passenger{ get; set; }
        public Booking (){}
        public Booking(string bookingID, string classBook, Flight flight, DateTime bookingDate, Passenger passenger){
            ValidData(bookingID,classBook,bookingDate);
            Flight = flight;
            Passenger = passenger;
        }
        //Checking the data validty 
        public void ValidData(string bookingID,string classBook,DateTime bookingDate){
            BookingID = !string.IsNullOrEmpty(bookingID) 
                ? bookingID 
                : throw new ArgumentNullException(nameof(bookingID), "Booking ID cannot be empty");
            
            if (Enum.TryParse(classBook, out BookingClass bookingClass) && Enum.IsDefined(typeof(BookingClass), bookingClass))
                Class = bookingClass; // Assign the parsed enum value
            else throw new ArgumentException("Class not valid", nameof(classBook));
            
            BookingDate = bookingDate > DateTime.Now ? bookingDate : throw new ArgumentException("you cann't book in past");

        }
        public void ViewBookingDetails()
        {
            Console.WriteLine($"Booking ID: {BookingID}" + $"Passenger Name: {Passenger.Name}"+
            $"Flight ID: {Flight.FlightID}" + $"Class: {Class}" + 
            $"Booking Date: {BookingDate}" + 
            $"Departure: {Flight.DepartureCountry} -> {Flight.DestinationCountry}");
        }
    }
}