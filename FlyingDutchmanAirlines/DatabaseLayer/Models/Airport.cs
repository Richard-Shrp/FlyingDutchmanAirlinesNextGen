﻿using System;
using System.Collections.Generic;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models;

public sealed class Airport
{
    public int AirportId { get; set; }

    public string City { get; set; }

    public string Iata { get; set; }

    public ICollection<Flight> FlightDestinationNavigations { get; set; }

    public ICollection<Flight> FlightOriginNavigations { get; set; }
	
	public Airport() {
		FlightDestinationNavigations = new HashSet<Flight>();
		FlightOriginNavigations = new HashSet<Flight>();
		City = string.Empty;
		Iata = string.Empty;
		AirportId = default;
	}
}
