using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BookingSystem
{
    public static class CsvParser
    {
        public static List<Flight> ParseFlights(string filePath)
        {
            var flights = new List<Flight>();

            using (var reader = new StreamReader(filePath))
            {
                
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length != 8)
                        throw new InvalidDataException("CSV file format is invalid.");

                    var flight = new Flight
                    {
                        FlightID = values[0],
                        DepartureCountry = values[1],
                        DestinationCountry = values[2],
                        DepartureDate = DateTime.Parse(values[3], CultureInfo.InvariantCulture),
                        DepartureAirport = values[4],
                        ArrivalAirport = values[5],
                        Price = decimal.Parse(values[6], CultureInfo.InvariantCulture),
                        Class = values[7]
                    };

                    flights.Add(flight);
                }
            }

            return flights;
        }

        public static List<Booking> ParseBookings(string filePath)
        {
            var bookings = new List<Booking>();

            using (var reader = new StreamReader(filePath))
            {
                
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length != 5)
                        throw new InvalidDataException("CSV file format is invalid.");

                    var booking = new Booking
                    {
                        BookingID = values[0],
                        Class = values[1],
                        Flight = new Flight { FlightID = values[2] }, 
                        BookingDate = DateTime.Parse(values[3], CultureInfo.InvariantCulture),
                        Passenger = new Passenger { PassengerID = double.Parse(values[4]) } 
                    };

                    bookings.Add(booking);
                }
            }

            return bookings;
        }
    }
}
