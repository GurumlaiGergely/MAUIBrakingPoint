using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrakingPoint.Models
{
    internal class DriverModel
    {
        public class Rootobject
        {
            public Mrdata MRData { get; set; }
        }

        public class Mrdata
        {
            public string xmlns { get; set; }
            public string series { get; set; }
            public string url { get; set; }
            public string limit { get; set; }
            public string offset { get; set; }
            public string total { get; set; }
            public Drivertable DriverTable { get; set; }
        }

        public class Drivertable
        {
            public string season { get; set; }
            public string driverId { get; set; }
            public Driver[] Drivers { get; set; }
        }

        public class Driver
        {
            public string driverId { get; set; }
            public string permanentNumber { get; set; }
            public string code { get; set; }
            public string url { get; set; }
            public string givenName { get; set; }
            public string familyName { get; set; }
            public string dateOfBirth { get; set; }
            public string nationality { get; set; }
        }
    }
}
