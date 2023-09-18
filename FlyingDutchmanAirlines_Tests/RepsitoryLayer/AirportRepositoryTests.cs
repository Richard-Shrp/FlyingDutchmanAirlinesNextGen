using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlinesContext_Tests.Stubs;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.DatabaseLayer.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using System.Runtime.ExceptionServices;
using System.Collections;

namespace FlyingDutchmanAirlines_Tests.RepositoryLayer {
	
	[TestClass]
	public class AirportRepositoryTests {
		private FlyingDutchmanAirlinesContext _context;
		private AirportRepository _repository;
		
		[TestInitialize]
		public async Task TestInitialize() {
			DbContextOptions<FlyingDutchmanAirlinesContext> 
				dbContextOptions = 
				new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
				.UseInMemoryDatabase("FlyingDutchman").Options;
			
			_context = new FlyingDutchmanAirlinesContext_Stub(dbContextOptions);
				
			SortedList<string, Airport> airports = new SortedList<string, Airport> {
				{
					"GOH",
					new Airport {
						AirportId = 1,
						City = "Nuuk",
						 Iata = "GOH" }
				},
					{
						"PHX",
						new Airport {
						  AirportId = 2,
						  City = "Phoenix",
						  Iata = "PHX" }
				},
					  {
						"DDH",
						new Airport {
						  AirportId = 3,
						  City = "Bennington",
						  Iata = "DDH"
						}
				},
					  {
						"RDU",
						new Airport {
						  AirportId = 4,
						  City = "Raleigh-Durham",
						  Iata = "RDU"
						}
					  }
		};
			
			_context.Airports.AddRange(airports.Values);
			await _context.SaveChangesAsync();
		
			_repository = new AirportRepository(_context);
			Assert.IsNotNull(_repository);
			
		}
		
		[TestMethod]
		[DataRow(1)]
		[DataRow(2)]
		[DataRow(3)]
		[DataRow(4)]
		public async Task GetAirportByID_Success(int airportId) {
			Airport airport = await _repository.GetAirportByID(airportId);
			Assert.IsNotNull(airport);
			
			Airport dbAirport = 
				_context.Airports.First(a => a.AirportId == airportId);
			
			Assert.AreEqual(dbAirport.AirportId, airport.AirportId);
			Assert.AreEqual(dbAirport.City, airport.City);
			Assert.AreEqual(dbAirport.Iata, airport.Iata);
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public async Task GetAirportByID_Failure_InvalidInput() {
			StringWriter outputStream = new StringWriter();
			try {
				Console.SetOut(outputStream);
				await _repository.GetAirportByID(-1);
			}
			catch(ArgumentException exception) {	
				Assert.IsTrue(outputStream.ToString().
				Contains("Argument Exception in GetAirportByID! AirportID = -1"));
				
				//ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
				throw new ArgumentException();			
			}
			finally {
				outputStream.Dispose();
			}
		}
		
		[TestMethod]
		[ExpectedException(typeof(AirportNotFoundException))]
		public async Task GetAirportByID_Failure_DatabaseException() {
			await _repository.GetAirportByID(10);
		}
	}
}
