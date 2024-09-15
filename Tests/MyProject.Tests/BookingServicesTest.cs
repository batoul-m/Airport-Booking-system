using Xunit;
using Moq;
using FluentAssertions;
using AutoFixture;
using BookingSystem;
public class BookingServicesTestsShould
{
    private readonly Fixture _fixture;
    private readonly Mock<IFileDataAccess> _mockDataAccess;
    private readonly BookingServices _bookingServices;
    public BookingServicesTestsShould()
    {
        _fixture = new Fixture();
        _mockDataAccess = new Mock<IFileDataAccess>();
        _bookingServices = new BookingServices(_mockDataAccess.Object);
    }
    [Fact]
    public void AddBookingAndSaveIt(){
        //Arrange
        var passenger = _fixture.Create<Passenger>();
        var flight = _fixture.Create<Flight>();
        var ClassType = "Economy";
        //Act
        var result = _bookingServices.BookFlight(passenger,flight,ClassType);
        //Assert
        result.Should().NotBeNull();
        result.BookingID.Should().NotBeNullOrEmpty();
        result.Class.Should().Be(BookingClass.Economy);
        _mockDataAccess.Verify(x => x.SaveBookings(It.IsAny<List<Booking>>()), Times.Once);

    }
    [Fact]
    public void RemoveBookingIfExists(){
        //Arrange
        var bookings = _fixture.CreateMany<Booking>(3).ToList();
        var bookingToRemove = bookings[1];
        _mockDataAccess.Setup(d => d.SaveBookings(It.IsAny<List<Booking>>()))
            .Callback<List<Booking>>(savedBookings =>
            {
                bookings = savedBookings;
            });
        bookings.ForEach(b => _bookingServices.BookFlight
                        (b.Passenger, b.Flight, b.Class.ToString()));

        //Act
        _bookingServices.CancelBooking(bookingToRemove.BookingID);

        //Assert
        bookings.Should().NotContain(bookingToRemove);
        _mockDataAccess.Verify(d => d.SaveBookings(It.IsAny<List<Booking>>()),Times.AtLeastOnce);

    }
    [Fact]
    public void ModifiyFlightAndClass(){
        //Arrange
        var bookings = _fixture.CreateMany<Booking>(3).ToList();
        var bookingToModifiy = bookings[1];
        var newFlight = _fixture.Create<Flight>();
        var newClass = "Business";
        _mockDataAccess.Setup(d => d.SaveBookings(It.IsAny<List<Booking>>()));

        bookings.ForEach(b => _bookingServices.BookFlight(b.Passenger, b.Flight, b.Class.ToString()));
        
        //Act
        _bookingServices.ModifyBooking(bookingToModifiy.BookingID,newFlight,newClass);

        //Assert
        bookingToModifiy.Flight.Should().Be(newFlight);
        bookingToModifiy.Class.Should().Be(BookingClass.Business);

        _mockDataAccess.Verify(d => d.SaveBookings(It.IsAny<List<Booking>>()), Times.AtLeastOnce);
    }

    public void GetBookingsByCriteria(){
    // Arrange
    var passengerId = "12345";
    var expectedBookings = new List<Booking>
    {
        new Booking {Flight = new Flight(
                    flightID: "F123", 
                    departureAirport: "JFK", 
                    departureCountry: "USA", 
                    destinationCountry: "Palestine", 
                    arrivalAirport: "TLV", 
                    departureDate: DateTime.Now, 
                    price: 500.00m, 
                    classFlight: "Economy"
                ),
                Passenger = new Passenger(
                    name: "John Doe", 
                    passengerID: passengerId, 
                    passportNumber: 12
                ) },
        new Booking     {
        Flight = new Flight(
            flightID: "F456", 
            departureAirport: "LAX", 
            departureCountry: "USA", 
            destinationCountry: "Germany", 
            arrivalAirport: "FRA", 
            departureDate: DateTime.Now.AddDays(1), 
            price: 650.00m, 
            classFlight: "Business"
        ),
        Passenger = new Passenger(
            name: "Jane Smith", 
            passengerID: passengerId, 
            passportNumber: 50
        )
    }
    };
    var mockBookings = new List<Booking>(expectedBookings);
    // Act
    var result = _bookingServices.GetBookingsByCriteria(null,passengerId,null);

    // Assert
    result.Should().HaveCount(2);
    result.Should().BeEquivalentTo(expectedBookings);
}  
}
