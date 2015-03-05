﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace GeoCodingInterface
{
    // Klasse entspricht den Parametern des EPSG Objects das vom REST zurückgeliefert wird.
    class epsg
    {
        [DeserializeAs(Name = "epsg")]
        public string epsgcode { get; set; }


    }
}
