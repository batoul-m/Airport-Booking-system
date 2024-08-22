using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookingSystem
{
    public class FlightService
    {
        private List<Flight> flights;
        public FlightService()
        {
            flights = new List<Flight>();
        }

        public List<Flight> SearchFlights(string departureCountry, string destinationCountry, DateTime departureDate, string classType, decimal maxPrice)
        {
            return flights.Where(f =>
                (string.IsNullOrEmpty(departureCountry) || f.DepartureCountry.Equals(departureCountry, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(destinationCountry) || f.DestinationCountry.Equals(destinationCountry, StringComparison.OrdinalIgnoreCase)) &&
                (departureDate == default || f.DepartureDate.Date == departureDate.Date) &&
                (string.IsNullOrEmpty(classType) || f.Class.Equals(classType, StringComparison.OrdinalIgnoreCase)) &&
                (maxPrice <= 0 || f.Price <= maxPrice)
            ).ToList();
        }

        public void AddFlight(Flight flight)
        {
            if (flight == null)
                throw new ArgumentNullException(nameof(flight), "Flight cannot be null.");

            var validationErrors = ValidateFlightData(flight);
            if (validationErrors.Any())
            {
                throw new InvalidOperationException("Cannot add flight. Validation errors: " + string.Join(", ", validationErrors));
            }

            flights.Add(flight);
        }

        public void ImportFlightsFromCsv(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("The specified file was not found.", filePath);

            var lines = File.ReadAllLines(filePath);
            if (lines.Length <= 1) 
                throw new InvalidDataException("CSV file is empty or missing data.");

            var header = lines[0].Split(',');
            if (header.Length < 8) 
                throw new InvalidDataException("CSV file format is invalid.");

            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');
                if (values.Length != header.Length)
                    throw new InvalidDataException("CSV file format is inconsistent.");

                if (values.Length < 8)
                    throw new InvalidDataException("CSV file is missing some columns.");

                DateTime departureDate;
                decimal price;

                if (!DateTime.TryParse(values[3], out departureDate))
                {
                    throw new FormatException("Invalid date format in CSV file.");
                }

                if (!decimal.TryParse(values[6], out price))
                {
                    throw new FormatException("Invalid price format in CSV file.");
                }

                var flight = new Flight
                {
                    FlightID = values[0],
                    DepartureCountry = values[1],
                    DestinationCountry = values[2],
                    DepartureDate = departureDate,
                    DepartureAirport = values[4],
                    ArrivalAirport = values[5],
                    Price = price,
                    Class = values[7]
                };

                AddFlight(flight);
            }
        }

        public List<string> ValidateFlightData(Flight flight)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(flight.FlightID))
                errors.Add("FlightID is required.");
            if (string.IsNullOrEmpty(flight.DepartureCountry))
                errors.Add("DepartureCountry is required.");
            if (string.IsNullOrEmpty(flight.DestinationCountry))
                errors.Add("DestinationCountry is required.");
            if (flight.DepartureDate == default)
                errors.Add("DepartureDate is required.");
            if (string.IsNullOrEmpty(flight.DepartureAirport))
                errors.Add("DepartureAirport is required.");
            if (string.IsNullOrEmpty(flight.ArrivalAirport))
                errors.Add("ArrivalAirport is required.");
            if (flight.Price <= 0)
                errors.Add("Price must be greater than zero.");

            return errors;
        }
    }
}
