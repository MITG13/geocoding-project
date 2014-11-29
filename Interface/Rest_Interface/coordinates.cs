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
    
    public coordinates() 
    { 
    }

    }


    public class codingResponse : List<coordinates>
    {
        public coordinates latlng { get; set; }
    }

    public class latlng : IRestResponse<codingResponse>
    {

    }

    public class codedAddress
    {
        public float latitude { get; set; }
        public float longitude { get; set; }

        public codedAddress() { }

        public codedAddress(codingResponse inputItem)
        {
            latitude = inputItem.latlng.lat;
            longitude = inputItem.latlng.lon;
        }

    }
}
