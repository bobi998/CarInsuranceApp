using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CarInsurance
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Egn { get; set; }

        public Customer()
        {

        }
        public Customer(string name , string egn)
        {
            this.Name = name;
            this.Egn = egn;
        }

        public static bool ValidateCustomerData(string name, string egn)
        {
            return (name.Length > 10 && egn.Length == 10);
        }
    }
}
