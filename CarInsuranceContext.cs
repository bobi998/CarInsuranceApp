using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInsurance
{
    class CarInsuranceContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Insurance> Insurances { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public CarInsuranceContext() : base(Properties.Settings.Default.InsuranceDbConnect)
        {

        }
    }
}
