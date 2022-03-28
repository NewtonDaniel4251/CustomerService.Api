using CustomerService.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Core.Interface
{
    public interface ICustomerRepository
    {
        Task<string> CustomerOnBoarding(CustomerDetailsVm entity);
        Task<string> ConfirmOtp(string Otp, int customerId);
        Task<List<CustomerDetailsVm>> GetAllExistingOnboardedCustomers();
        Task<decimal> GoldPrice();
    }
}
