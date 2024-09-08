using System;
using System.Globalization;
namespace BookingSystem{
    public class CsvParser : ICsvParser{
        public List<Flight> ParseFlights(string filePath){
            var flights = new List<Flight>();
            using (var reader = new StreamReader(filePath)){
                reader.ReadLine();
                while (!reader.EndOfStream){
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values.Length != 8)
                        throw new InvalidDataException("CSV file format is invalid.");
                    var flight = new Flight(
                    flightID: values[0],
                    departureAirport: values[4],
                    departureCountry: values[1],
                    destinationCountry: values[2],
                    arrivalAirport: values[5],
                    departureDate: DateTime.Parse(values[3], CultureInfo.InvariantCulture),
                    price: decimal.Parse(values[6], CultureInfo.InvariantCulture),
                    classFlight: values[7]
                );

                    flights.Add(flight);
                }
            }return flights;
        }
        public List<Booking> ParseBookings(string filePath)
        {
            var bookings = new List<Booking>();
            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream){
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values.Length != 5)
                        throw new InvalidDataException("CSV file format is invalid.");
                    var booking = new Booking(
                        bookingID: values[0],
                        classBook: values[1],
                        flight: new Flight(
                            flightID: values[2],  
                            departureAirport: values[3],  
                            departureCountry: values[4],  
                            destinationCountry: values[5],  
                            arrivalAirport: values[6],  
                            departureDate: DateTime.Parse(values[7], CultureInfo.InvariantCulture),  
                            price: decimal.Parse(values[8], CultureInfo.InvariantCulture),  
                            classFlight: values[1]  ),
                        bookingDate: DateTime.Parse(values[7], CultureInfo.InvariantCulture),
                        passenger: new Passenger (
                            name: values[9],            
                            passengerID: values[10],      
                            passportNumber: double.Parse(values[11]) )
                    );
                    bookings.Add(booking);
                }
            }return bookings;
        }
    }
}
