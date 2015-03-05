using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace GeoCodingInterface
{
    // Klasse für das deserialisieren der Provider. Entspricht dem zurückgelieferten Objekt vom GetProviders Request
    class servicelist
    {

        [DeserializeAs(Name = "providers")]
        public List<string> providers { get; set; }
    }

}
