using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInsurance
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }

        public string Color { get; set; }
        public VehicleType? VehType { get; set; }
        public CoupeType? CoupeType { get; set; }

        public int Power { get; set; }

        public string FuelType { get; set; }
        public string VIN { get; set; }

        public int OwnerID { get; set; }

        public Vehicle()
        {

        }
        public Vehicle(string brand , string model , int year , int month , string color , VehicleType vehtype , CoupeType coupetype , int power, string fuel , string vin , int ownerID)
        {
            this.Brand = brand;
            this.Model = model;
            this.Year = year;
            this.Month = month;
            this.Color = color;
            this.VehType = vehtype;
            this.CoupeType = coupetype;
            this.Power = power;
            this.FuelType = fuel;
            this.VIN = vin;
            this.OwnerID = ownerID;
        }

        public static bool ValidateVehicleData(string brand, string model, int year, int month, string color, string vin)
        {
            return (brand.Length >= 2 && model.Length > 0 && year > 1930 && year < DateTime.Now.Year && month > 0 && month <= DateTime.Now.Month && color.Length > 2 && vin.Length == 20);
        }
    }
}
