using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace GeoCodingInterface
{
    class coordinates
    {
        public float lat { get; set; }
        public float lon { get; set; }
    
        public coordinates() { }

        public coordinates(codingResponse inputItem)
        {
            lat = inputItem.latlng.lat;
            lon = inputItem.latlng.lon;
        }

    }


    class codingResponse : List<coordinates>
    {
        public coordinates latlng { get; set; }
    }

    
}
