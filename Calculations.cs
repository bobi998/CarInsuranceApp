using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInsurance
{
    static class Calculations
    {
        static public double CalcLiabPrice(VehicleType vehType , int Exp, int vehPower , int EngCap , int FuelType , int vehYear)
        {
            double price = 0.0;

            if (15 < (DateTime.Now.Year - vehYear))
            {
                price = price + 150;
            }
            else
            {
                price = price + 100;
            }
            if (150 <= vehPower)
            {
                price = price + 50;
            }
            else
            {
                price = price + 25;
            }

            if (5 < Exp)
            {
                price = price * 0.9;
            }

            switch (FuelType)
            {
                case 0:
                    {
                        price = price * 1.1;
                        price = price + EngCap / 100;
                    }
                    break;
                case 1:
                    {
                        price = price * 1.2;
                        price = price + EngCap / 100;
                    }
                    break;
                default:
                    {
                        price = price * 0.7;
                    }
                    break;
            }

            switch (vehType)
            {
                case 0:
                    {
                        price = price + 10;
                    }
                    break;
                case (VehicleType)1:
                    {
                        price = price + 20;
                    }
                    break;
                case (VehicleType)2:
                    {
                        price = price + 30;
                    }
                    break;
                default:
                    {
                        price = price + 50;
                    }
                    break;
            }

            return price;
        }

        static public double CalcCascoPrice(int experiance , VehicleType vehType , int vehPower , int vehYear , double insureMoney , bool otherInsurance)
        {
            double price = 0.0;

            if( 15 < (DateTime.Now.Year - vehYear))
            {
                price = price + 150;
            }
            else
            {
                price = price + 100;
            }
            if( 150 <= vehPower)
            {
                price = price + 50;
            }
            else
            {
                price = price + 25;
            }

            price = price + insureMoney / 100;

            if(5 < experiance)
            {
                price = price * 0.9;
            }
            if(otherInsurance)
            {
                price = price * 0.95;
            }

            switch (vehType)
            {
                case 0:
                    {
                        price = price + 10;
                    }
                    break;
                case (VehicleType)1:
                    {
                        price = price + 20;
                    }
                    break;
                case (VehicleType)2:
                    {
                        price = price + 30;
                    }
                    break;
                default:
                    {
                        price = price + 50;
                    }
                    break;
            }

            return price;
        }
    }
}
