using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CustomerService.Core.Models
{
    public partial class CustomerDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        //public string State { get; set; }
        //public string Lga { get; set; }
        public string OTP { get; set; }
        public bool IsOTPValidated { get; set; }
        //[ForeignKey("TBL_STATE")]
        //public int StateId { get; set; }
        public DateTime OtpGeneratedDateTime { get; set; }
        public State TBL_STATE { get; set; }
    }

    public class State
    {
        [Key]
        public int StateId { get; set; }
        [Required]
        public string StateName { get; set; }
        public ICollection<LGA> TBL_Lgas { get; set; }
    }

    public class LGA
    {
        [Key]
        public int LGAId { get; set; }
        [Required]
        public string LGAName { get; set; }
        [ForeignKey("TBL_State")]
        public int StateId { get; set; }
        [ForeignKey("TBL_Customer_Detail")]
        public int CustomerId { get; set; }
        public CustomerDetail TBL_Customer_Detail { get; set; }
       
        public State TBL_State { get; set; }
    } 
}
