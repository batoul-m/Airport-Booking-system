using Xunit;
using Moq;
using FluentAssertions;
using AutoFixture;
using BookingSystem;
using System;
using System.Collections.Generic;
public class FlightServicesTestsShould
{
    private readonly Fixture _fixture;
    private readonly Mock<IFileDataAccess> _mockDataAccess;
    private readonly FlightService _flightServices;
    public FlightServicesTestsShould(){
        _fixture = new Fixture();
        _mockDataAccess = new Mock<IFileDataAccess>();
        var _flightServices = new FlightService(_mockDataAccess.Object);
    }
    [Fact]
    public void ShouldAddFlight(){
        //Arrange
        var flight = _fixture.Create<Flight>();
        //Act
        _flightServices.AddFlight(flight);
        var result = _flightServices.SearchFlights(flight.DepartureCountry,flight.DestinationCountry,flight.DepartureDate,flight.Price);
        //Assert
        result.Should().HaveCount(1);
        result.Should().Equal(flight);
        _mockDataAccess.Verify(x => x.SaveFlights(It.IsAny<List<Flight>>()),Times.Once);
    }
    [Fact]
    public void ImportFlightsFromCsv(){
        // Arrange
        var flightsFromCsv = _fixture.CreateMany<Flight>(5).ToList(); 
        var filePath = "dummyFilePath.csv";
        _mockDataAccess.Setup(x => x.LoadFlights(filePath)).Returns(flightsFromCsv);

        // Act
        _flightServices.ImportFlightsFromCsv(filePath);
        var result = _flightServices.SearchFlights(null, null, default, 0);

        // Assert
        result.Should().HaveCount(5); 
        result.Should().BeEquivalentTo(flightsFromCsv);
    }
    [Fact]
    public void SearchFlightsById(){
        //Arrange
        var flight = _fixture.Create<Flight>();
        flight.FlightID = "12";

        //Act
        var result = _flightServices.SearchFlightsById(flight.FlightID);
        //Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(flight);
    }
 
}
