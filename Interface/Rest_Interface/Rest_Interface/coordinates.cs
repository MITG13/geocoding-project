using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace GeoCodingInterface
{
    // Klasse entspricht den Parametern des Geometry Objects das vom REST zurückgeliefert wird.
    class geometrydetails
    {
        
        [DeserializeAs(Name = "coordinates")]
        public List<string> coordinates { get; set; }
        [DeserializeAs(Name = "type")]
        public string type { get; set; }


    }


    
}
