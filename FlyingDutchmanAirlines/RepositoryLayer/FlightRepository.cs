using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer {
    public class FlightRepository {
		private readonly FlyingDutchmanAirlinesContext _context;
		
		public FlightRepository() {
			_context = new FlyingDutchmanAirlinesContext();
		}
		
		public FlightRepository(FlyingDutchmanAirlinesContext context) {
			_context = context;
		}
		
		public async Task<Flight> GetFlightByFlightNumber(int flightNumber, int originAirportId, int destinationAirportId) {
				if(!originAirportId.IsPositive() || !destinationAirportId.IsPositive()) {
					Console.WriteLine(
					$"Argument Exception in GetFlightByFlightNumber! originAirportId = {originAirportId} : destinationAirportId = {destinationAirportId}");
					throw new ArgumentException("invalid arguments provided");
				} 
				if(!flightNumber.IsPositive()) {
					Console.WriteLine($"Could not find flight in GetFlightByFlightNumber! flightNumber = {flightNumber}");
					throw new FlightNotFoundException();
				}
				
				return await _context.Flights.FirstOrDefaultAsync(f => f.FlightNumber == flightNumber) ??
					throw new FlightNotFoundException();			
		}
		
    }
}
