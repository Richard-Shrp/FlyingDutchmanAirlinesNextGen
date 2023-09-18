using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlinesContext_Tests.Stubs;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.ServiceLayer;

using Moq;
using System;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines_Tests.ServiceLayer {
	
	[TestClass]
	public class BookingServiceTests {
		private FlyingDutchmanAirlinesContext _context;
		
		[TestInitialize]
		public void TestInitialize() {
		}
		
		[TestMethod]
		public async Task CreateBooking_Success() {
			Mock<BookingRepository> mockBookingRepository = new Mock<BookingRepository>();
			Mock<CustomerRepository> mockCustomerRepository = new Mock<CustomerRepository>();
			
			
			mockBookingRepository.Setup(repository => 
				repository.CreateBooking(0, 0)).Returns(Task.CompletedTask);
			mockCustomerRepository.Setup(repository => 
				repository.GetCustomerByName("Leo Tolstoy"))
				.Returns(Task.FromResult(new Customer("Leo Tolstoy")));
			
			
			BookingService service = new BookingService(mockBookingRepository.Object, mockCustomerRepository.Object);
			(bool result, Exception exception) = 
				await service.CreateBooking("Leo Tolstoy", 0);
			
			Assert.IsTrue(result);
			Assert.IsNull(exception);
		} 
		
		[TestMethod]
		[DataRow("", 0)]
		[DataRow(null, -1)]
		[DataRow("Emily Noether", -2)]
		public async Task CreateBooking_Failure_InvalidInputArguments(string name, int flighNumber) {
			Mock<BookingRepository> mockBookingRepository = new Mock<BookingRepository>();
			Mock<CustomerRepository> mockCustomerRepository = new Mock<CustomerRepository>();
			
			BookingService service = new BookingService(mockBookingRepository.Object, mockCustomerRepository.Object);
			
			(bool result, Exception exception) = await service.CreateBooking(name, flightNumber);
			
			Assert.IsFalse(result);
			Assert.IsNotNull(exception);
		}
	}
}





























