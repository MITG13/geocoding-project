using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace GeoCodingInterface
{
    // Klasse entspricht den Parametern des Property Objects das vom REST zurückgeliefert wird.
    class addressdata
    {
        [DeserializeAs(Name = "address")]
        public string address { get; set; }
        [DeserializeAs(Name = "country")]
        public string country { get; set; }
        [DeserializeAs(Name = "zip")]
        public string zip { get; set; }
        [DeserializeAs(Name = "city")]
        public string city { get; set; }
        [DeserializeAs(Name = "street")]
        public string street { get; set; }
        [DeserializeAs(Name = "housenumber")]
        public string housenumber { get; set; }

    }

    class addressdat
    {
        public string address { get; set; }
        public string country { get; set; }
        public string zip { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string housenumber { get; set; }

        public addressdat(string addressstring)
        {

            this.address = addressstring;
        }
    }


}
