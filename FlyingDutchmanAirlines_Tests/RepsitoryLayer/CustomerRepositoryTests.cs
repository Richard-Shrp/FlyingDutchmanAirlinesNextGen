using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlinesContext_Tests.Stubs;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlyingDutchmanAirlines_Tests.RepositoryLayer;

[TestClass]
public class CustomerRepositoryTests
{
    private FlyingDutchmanAirlinesContext _context = 
		new FlyingDutchmanAirlinesContext();
		
	private CustomerRepository _repository = new CustomerRepository();
	
	
	[TestInitialize]
	public async Task TestInitialize() {
		DbContextOptions<FlyingDutchmanAirlinesContext> dbContextOptions = new
		DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>()
		.UseInMemoryDatabase("FlyingDutchman").Options;
		
		_context = new FlyingDutchmanAirlinesContext(dbContextOptions);
		
		Customer testCustomer = new Customer("Linus Torvalds");
		_context.Customers.Add(testCustomer);
		await _context.SaveChangesAsync();
		
		_repository = new CustomerRepository(_context);
		Assert.IsNotNull(_repository);
	}
	
	[TestMethod]
    public async Task CreateCustomer_Succes() {		
		bool result = await _repository.CreateCustomer("Michael Groves");
		Assert.IsTrue(result);
	}
	
	[TestMethod]
	public async Task CreateCustomer_Failure_NameIsNull() { 
		bool result = await _repository.CreateCustomer(string.Empty);
		Assert.IsFalse(result);
	} 
	
	[TestMethod]
	public async Task CreateCustomer_Failure_NameIsEmptyString() {
		bool result = await _repository.CreateCustomer(string.Empty);
		Assert.IsFalse(result);
	} 
	
	[TestMethod]
	[DataRow('#')]
	[DataRow('$')]
	[DataRow('%')]
	[DataRow('&')]
	[DataRow('*')]
	public async Task CreateCustomer_Failure_NameContainsInvalidCharacters(char invalidCharacter) {		
		bool result = await _repository.CreateCustomer("Donald Knuth" + invalidCharacter);
		Assert.IsFalse(result);
	}
	
	[TestMethod]
	[DataRow("")]
	[DataRow(null)]
	[DataRow("#")]
	[DataRow("$")]
	[DataRow("%")]
	[DataRow("&")]
	[DataRow("*")]
	[ExpectedException(typeof(CustomerNotFoundException))]
	public async Task GetCustomerByName_Failure_InvalidName(string name) {
		await _repository.GetCustomerByName(name);
	} 
	
	[TestMethod]
	public async Task CreateCustomer_Failure_DatabaseAccesError() {
		CustomerRepository _repository = new CustomerRepository(null);
		Assert.IsNotNull(_repository);
		
		bool result = await _repository.CreateCustomer("Michael Ryan-Ruiz");
		Assert.IsFalse(result);
	}
	
	[TestMethod]
	public async Task GetCustomerByName_Success() {
		Customer customer = 
			await _repository.GetCustomerByName("Linus Torvalds");
		Assert.IsNotNull(customer);
	}
		
}