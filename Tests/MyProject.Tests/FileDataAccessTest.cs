using Xunit;
using Moq;
using FluentAssertions;
using AutoFixture;
using BookingSystem;
public class FileDataAccessTestShould{
    private readonly Fixture _fixture;
    private readonly FileDataAccess _fileDataAccess;
    private readonly Mock<ICsvParser> _mockCsvParser;
    public FileDataAccessTestShould(){
        _mockCsvParser = new Mock<ICsvParser>();
        _fixture = new Fixture();
        _fileDataAccess = new FileDataAccess(_mockCsvParser.Object);
    }
    [Fact]
    public void ThrowExeptionWhenFlightsFileNotExit(){
        //Arrange
        var filePath = "nonExit.csv";
        var exceptionMessage = $"Flights file not found. ({filePath})";

        //Act
        Action action= () => _fileDataAccess.LoadFlights(filePath);

        //Assert
        action.Should().Throw<FileNotFoundException>()
                        .WithMessage(exceptionMessage);

    }
    [Fact]
    public void ThrowExeptionWhenFlightsFileIsEmpty(){
        // Arrange
        var filePath = "empty.csv";
        File.WriteAllText(filePath, ""); 
        var exceptionMessage = "CSV file is empty or missing data.";

        // Act
        Action action = () => _fileDataAccess.LoadFlights(filePath);

        // Assert
        action.Should().Throw<InvalidDataException>()
            .WithMessage(exceptionMessage);
    }
    [Fact]
    public void LoadFlightFromValidFlightsFile(){
        // Arrange
        var filePath = "validFlights.csv";
        var validCsvContent = "FlightID,DepartureCountry,DestinationCountry,DepartureDate,DepartureAirport,ArrivalAirport,Price,Class";
        File.WriteAllText(filePath, validCsvContent); 
        var expectedFlights = _fixture.Create<List<Flight>>();

        _mockCsvParser.Setup(p => p.ParseFlights(filePath)).Returns(expectedFlights);

        // Act
        var result = _fileDataAccess.LoadFlights(filePath);

        // Assert
        result.Should().BeEquivalentTo(expectedFlights);
    }
    [Fact]
    public void SaveFlights(){
        //Arrange
        var filePath = "filePate.csv";
        var flights = _fixture.Create<List<Flight>>();

        //Act
        _fileDataAccess.SaveFlights(flights);

        //Assert
        File.Exists(filePath).Should().BeTrue();
        var lines = File.ReadAllLines(filePath);
        lines.Should().HaveCount(flights.Count+1);
    }
    [Fact]
    public void ThrowExeptionWhenBookingNotExit(){
        //Arrange
        var filePath = "nonExit.csv";
        var exceptionMessage = $"Bookings file not found. ({filePath})";

        //Act
        Action action= () => _fileDataAccess.LoadBookings(filePath);

        //Assert
        action.Should().Throw<FileNotFoundException>()
                        .WithMessage(exceptionMessage);

    }
    [Fact]
    public void ThrowExeptionWhenBookingFileIsEmpty(){
        // Arrange
        var filePath = "empty.csv";
        File.WriteAllText(filePath, ""); 
        var exceptionMessage = "CSV file is empty or missing data.";

        // Act
        Action action = () => _fileDataAccess.LoadBookings(filePath);

        // Assert
        action.Should().Throw<InvalidDataException>()
            .WithMessage(exceptionMessage);
    }
    [Fact]
    public void LoadBookingsFromValidFile(){
        // Arrange
        var filePath = "validFlights.csv";
        var validCsvContent = "FlightID,DepartureCountry,DestinationCountry,DepartureDate,DepartureAirport,ArrivalAirport,Price,Class";
        File.WriteAllText(filePath, validCsvContent); 
        var expectedBookings = _fixture.Create<List<Booking>>();

        _mockCsvParser.Setup(p => p.ParseBookings(filePath)).Returns(expectedBookings);

        // Act
        var result = _fileDataAccess.LoadBookings(filePath);

        // Assert
        result.Should().BeEquivalentTo(expectedBookings);
    }
    [Fact]
    public void SaveBookings(){
        //Arrange
        var filePath = "file.csv";
        var Bookings = _fixture.Create<List<Booking>>();

        //Act
        _fileDataAccess.SaveBookings(Bookings);

        //Assert
        File.Exists(filePath).Should().BeTrue();
        var lines = File.ReadAllLines(filePath);
        lines.Should().HaveCount(Bookings.Count+1);
    }

}