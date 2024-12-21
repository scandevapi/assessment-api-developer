using assessment_api_developer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace assessment_api_developer.API.Controllers
{
    [ApiController]
    [Route("api/V1/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


    }
}
