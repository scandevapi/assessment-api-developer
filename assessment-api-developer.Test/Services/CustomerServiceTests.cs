using assessment_api_developer.Domain.Interfaces;
using assessment_api_developer.Services.Services;
using Moq;

namespace assessment_api_developer.Test.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepository;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_mockRepository.Object);
        }

        [Fact]
        public void Test1()
        {

        }
    }
}