using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCodingInterface
{
    class service
    {
        [DeserializeAs(Name = "providers")]
        public string providers { get; set; }

    }   

}
