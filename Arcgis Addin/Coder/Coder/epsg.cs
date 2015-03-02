using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace GeoCodingInterface
{
    class epsg
    {
        [DeserializeAs(Name = "epsg")]
        public string epsgcode { get; set; }


    }
}
