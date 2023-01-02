using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrakingPoint.Models
{
    public class Drivers
    {
        public int ID { get; set; }
        public string driverID { get; set; }
        public int permanentNumber { get; set; }
        public string code { get; set; }
        public string url { get; set; }
        public string givenName { get; set; }
        public string familyName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string nationality { get; set; }

        public Drivers(int iD, string driverID, int permanentNumber, string code, string url, string givenName, string familyName, DateTime dateOfBirth, string nationality)
        {
            ID = iD;
            this.driverID = driverID;
            this.permanentNumber = permanentNumber;
            this.code = code;
            this.url = url;
            this.givenName = givenName;
            this.familyName = familyName;
            this.dateOfBirth = dateOfBirth;
            this.nationality = nationality;
        }
    }
}
