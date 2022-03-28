using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Core.ViewModels
{
   public class CustomerDetailsVm
    {
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string state { get; set; }
        public string lga { get; set; }
    }
}
