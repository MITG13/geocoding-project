using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace GeoCodingInterface
{
    class geometrydetails
    {

        [DeserializeAs(Name = "coordinates")]
        public List<string> coordinates { get; set; }
        [DeserializeAs(Name = "type")]
        public string type { get; set; }


    }


    
}
