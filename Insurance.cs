using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInsurance
{
    public class Insurance
    {
        public int InsuranceId { get; set; }
        public int? Id { get; set; }
        public int CustomerID { get; set; }
        public int VehicleID { get; set; }
        public double? PolicyPrice { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public InsuranceType? TypeOfInsurance { get; set; }
        public double? InsuranceMoney { get; set; }

        public Insurance()
        {

        }

        public Insurance(int id , int customerID , int vehicleID , double policyprize , DateTime issuedate , DateTime duedate , InsuranceType typeofinsurance , double insuranceMoney)
        {
            this.Id = id;
            this.CustomerID = customerID;
            this.VehicleID = vehicleID;
            this.PolicyPrice = policyprize;
            this.IssueDate = issuedate;
            this.DueDate = duedate;
            this.TypeOfInsurance = typeofinsurance;
            this.InsuranceMoney = insuranceMoney;
        }

        public static bool CheckInsuranceData(double policyprize, DateTime issuedate, DateTime duedate)
        {
            return (policyprize > 0 && issuedate.Month >= DateTime.Now.Month && issuedate.Day >= DateTime.Now.Day && duedate < DateTime.Now);
        }
    }
}
