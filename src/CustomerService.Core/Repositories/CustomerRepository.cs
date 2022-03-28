using CustomerService.Core.Interface;
using CustomerService.Core.Models;
using CustomerService.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerService.Core.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private CustomerRegistrationContext _context;

        public CustomerRepository(CustomerRegistrationContext context)
        {
            _context = context;
        }

        public async Task<string> CustomerOnBoarding(CustomerDetailsVm entity)
        {
            if (entity.phoneNumber == null)
                throw new Exception("Kindly Provide Your Phone Number");
            if (entity.email == null)
            {
                throw new Exception("Kindly Provide Your Email Address");
            }
            if (entity.password == null)
            {
                throw new Exception("Kindly Provide Your Password");
            }

            var response = String.Empty;

            CustomerDetail customer = new CustomerDetail()
            {
                PhoneNumber = entity.phoneNumber,
                Email = entity.email,
                Password = entity.password,
                //State = entity.state,
                //Lga = entity.lga
            };

            //hash the password

            //States
            var newState = new State();
            newState.StateName = entity.state;
            _context.States.Add(newState);
            _context.SaveChanges();


            var lga = new LGA()
            {
                LGAName = entity.lga,
                StateId = newState.StateId,
                CustomerId = customer.Id
            };
            _context.LGAs.Add(lga);

            var otp = GenerateOtp();
            customer.OTP = otp;
            customer.OtpGeneratedDateTime = DateTime.Now;
            customer.IsOTPValidated = false;

            _context.CustomerDetails.Add(customer);

            //send OTP to phone
            SendOTP(otp, entity.phoneNumber);

            var result = await _context.SaveChangesAsync() > 0;

            if (result)
                response = "Please check your phone to confirm your OTP";

            return response;
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            var otpNumber = random.Next(000000, 999999);

            return otpNumber.ToString();
        }


        private void SendOTP(string otp, string phoneNumber)
        {

        }

        public async Task<string> ConfirmOtp(string Otp, int customerId)
        {
            var expiresIn = 10; //expires in 10 minutes
            var customer = _context.CustomerDetails.FirstOrDefault(c => c.Id == customerId);
            var currentDate = DateTime.Now;
            //TimeSpan diff = TimeSpan.FromMinutes(currentDate - customer.OtpGeneratedDateTime);
            var diff = currentDate.Subtract(customer.OtpGeneratedDateTime);
            //confirm if otp has not expired based on timespan set
            if (diff.Minutes > expiresIn)
                throw new Exception("Otp has expired. Please Generate a new One.");

            if (Otp != customer.OTP)
                throw new Exception("Otp Confirmation unsuccessful!");

            customer.IsOTPValidated = true;

            await _context.SaveChangesAsync();

            return "Otp Confirmation successful!";
            
        }

        public async Task<List<CustomerDetailsVm>> GetAllExistingOnboardedCustomers()
        {
            var customers = await _context.CustomerDetails.Where(c => c.IsOTPValidated == true)
                            .Select(c => new CustomerDetailsVm
                            {
                                //state = _context.States.Where(s => s.StateId == c.StateId).Select(s =>s.StateName).FirstOrDefault(),
                                phoneNumber = c.PhoneNumber,
                                state = _context.LGAs.FirstOrDefault(l => l.CustomerId == c.Id).TBL_State.StateName,
                                email = c.Email,
                                lga = _context.LGAs.Where(l => l.CustomerId == c.Id).Select(r => r.LGAName).FirstOrDefault()

                            })
                             .ToListAsync();

            return customers;
        }

        public async Task<decimal> GoldPrice()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://gold-price-live.p.rapidapi.com/get_metal_prices"),
                Headers =
    {
        { "X-RapidAPI-Host", "gold-price-live.p.rapidapi.com" },
        { "X-RapidAPI-Key", "a98a2556a0msh8b0bd4267b4a0dep14147djsndc2a3e589831" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var metals = JsonSerializer.Deserialize<GoldPriceViewModel>(body);
                return metals.gold;
            }
        }
    }
}