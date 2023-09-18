using System;
using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models;

public sealed class Flight
{
    public int FlightNumber { get; set; }

    public int Origin { get; set; }

    public int Destination { get; set; }

    public ICollection<Booking> Bookings { get; set; }

    public Airport DestinationNavigation { get; set; }

    public Airport OriginNavigation { get; set; }
	
	public Flight(int flightNumber, int origin, int destination) {
		Bookings = new HashSet<Booking>();
		
		FlightNumber = flightNumber;
		Origin = origin;
		Destination = destination;
	} 
	
	public Flight() {
		Bookings = new HashSet<Booking>();
	}
}
