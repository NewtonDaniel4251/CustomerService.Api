using CustomerService.Core.Interface;
using CustomerService.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CustomerService.Api.Controllers
{
    [ApiController]
    [Route("api/customerservice/v1")]
    public class CustomerController : ControllerBase
    {
        private ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost("customer")]
        public async Task<IActionResult> RegisterCustomer(CustomerDetailsVm customer)
        {
          var result = await _customerRepository.CustomerOnBoarding(customer);

            return Ok( new { success = true, data = result});
        }

        [HttpGet("customers")]
        public async Task<IActionResult> Customers()
        {
            var result = await _customerRepository.GetAllExistingOnboardedCustomers();

            return Ok(new { success = true, data = result });
        }
        //Get Existing Gold Price
        [HttpGet("prices")]
        public async Task<IActionResult> GoldPrices()
        {
            var result = await _customerRepository.GoldPrice();

            return Ok(new { success = true, data = result });
        }
        [HttpPost("confirm-otp")]
        public async Task<IActionResult> ConfirmOtp(string Otp, int customerId)
        {
            var result = await _customerRepository.ConfirmOtp(Otp, customerId);

            return Ok(new { success = true, data = result });
        }
    }
}


