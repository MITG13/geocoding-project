using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace GeoCodingInterface
{
    class address
    {

        public string adress { get; set; }
        public string country { get; set; }
        public string zip { get; set; } 
        public string city { get; set; }
        public string street { get; set; }
        public string housenumber { get; set; } 
    }

}
