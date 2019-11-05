using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoreDocker.Models
{
    public class Customer
    {
        [Key]
        public string SecurityNumber { get; set; }
        [RegularExpression(@"(^\+[4][6][0-9]{9})")]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}
