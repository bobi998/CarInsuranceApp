using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarInsurance
{
    public class ManageDB
    {
        //Handles addition of customer in the database
        //No null check for argument since it is done before invokation of function
        static public void AddCustInDB(Customer cust)
        {
            CarInsuranceContext context = new CarInsuranceContext();
            context.Customers.Add(cust);
            context.SaveChanges();
        }

        //Handles addition of vehicle in the database
        //No null check for argument since it is done before invokation of function
        static public void AddVehInDB(Vehicle veh)
        {
            CarInsuranceContext context = new CarInsuranceContext();
            context.Vehicles.Add(veh);
            context.SaveChanges();
        }

        //Handles addition of insurance in the database
        //No null check for argument since it is done before invokation of function
        static public void AddInsuranceInDB(Insurance insure)
        {
            CarInsuranceContext context = new CarInsuranceContext();
            context.Insurances.Add(insure);
            context.SaveChanges();
        }

        //Get function for insurances count in the database
        static public int GetInsuranceCount()
        {
            CarInsuranceContext context = new CarInsuranceContext();
            return context.Insurances.Count();
        }

        //Handles fetching a customer from database by egn
        //Returns false if no customer is fetched
        static public bool CheckForOwnerInDB(string egn, ref Customer cust)
        {
            bool bRetVal = false;
            CarInsuranceContext context = new CarInsuranceContext();

            if (0 != context.Customers.Count())
            {
                Customer result = (from Cust in context.Customers
                                   where Cust.Egn == egn
                                   select Cust).FirstOrDefault();

                if (null != result)
                {
                    bRetVal = true;
                    cust = result;
                }
            }

            return bRetVal;
        }

        //Handles fetching a vehicle from database by egn
        //Returns false if no vehicle is fetched
        static public bool CheckForVehInDB(string vin, ref Vehicle veh)
        {
            bool bRetVal = false;
            CarInsuranceContext context = new CarInsuranceContext();

            if (0 != context.Vehicles.Count())
            {
                Vehicle result = (from Veh in context.Vehicles
                                  where Veh.VIN == vin
                                  select Veh).FirstOrDefault();

                if (null != result)
                {
                    bRetVal = true;
                    veh = result;
                }
            }

            return bRetVal;
        }

        //Get function for insurances count for specific client in the database
        //Returns -1 if client is not found in database
        static public int GetInsuranceCountByEgn(string egn)
        {
            int RetVal = 0;
            CarInsuranceContext context = new CarInsuranceContext();
            Customer result = (from Cust in context.Customers
                               where Cust.Egn == egn
                               select Cust).FirstOrDefault();

            if (null != result)
            {
                List<Insurance> insurances = context.Insurances.ToList();
                foreach (Insurance ins in insurances)
                {
                    if (ins.CustomerID == result.CustomerId)
                    {
                        RetVal++;
                    }
                }
            }
            else
            {
                RetVal = -1;
            }

            return RetVal;
        }

        //Get function for insurances for specific client in the database
        //No null check for argument since it is done before invokation of function
        static public void GetClientInsurances(string egn , ref List<Insurance> ListOfInsurances)
        {
            CarInsuranceContext context = new CarInsuranceContext();

            int clientId = (from Cust in context.Customers
                            where Cust.Egn == egn
                            select Cust.CustomerId).FirstOrDefault();

            ListOfInsurances = (from Insur in context.Insurances
                                where Insur.CustomerID == clientId
                                select Insur).ToList();
        }

        //Returns average price of the vehicles in the database
        static public double CalcAvgPriceOfVehicles()
        {
            double RetVal = 0.0;
            int count = 0;
            CarInsuranceContext context = new CarInsuranceContext();

            List<Insurance> insurances = (from Insur in context.Insurances
                                          where Insur.InsuranceMoney != 0
                                          select Insur).ToList();

            count = insurances.Count();

            foreach(Insurance ins in  insurances)
            {
                RetVal = RetVal + (double)ins.InsuranceMoney;
            }

            return RetVal/count;
        }

        //Returns average age of the vehicles in the database
        static public double CalcAvgAgeOfVehicles()
        {
            double RetVal = 0.0;
            int count = 0;
            int CurrYear = DateTime.Now.Year;

            CarInsuranceContext context = new CarInsuranceContext();

            List<Vehicle> vehicles = (from Veh in context.Vehicles
                                      select Veh).ToList();

            count = vehicles.Count();

            foreach(Vehicle veh in vehicles)
            {
                RetVal = (double)(RetVal + (CurrYear - veh.Year));
            }

            return RetVal/count;
        }

        //Get function for valid insurances for specific car in the database
        //No null check for argument since it is done before invokation of function
        static public bool CheckForValidInsurancesByVIN(string vin , ref List<Insurance> insurances)
        {
            bool bRetVal = false;
            DateTime CurrDate = DateTime.Now;
            CarInsuranceContext context = new CarInsuranceContext();

            int vehID = (from Veh in context.Vehicles
                         where Veh.VIN == vin
                         select Veh.VehicleId).FirstOrDefault();

            if(0 != vehID)
            {
                List<Insurance> Insurances = (from Insur in context.Insurances
                                              where Insur.VehicleID == vehID
                                              && DateTime.Compare((DateTime)Insur.DueDate , CurrDate) > 0
                                              select Insur).ToList();

                if(Insurances.Any())
                {
                    insurances = Insurances;
                }
                else
                {
                    insurances = null;
                }
                bRetVal = true;
            }

            return bRetVal;
        }
    }
}
