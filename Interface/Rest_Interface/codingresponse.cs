using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCodingInterface
{


    class codingObject
    {
        public addressdata properties { get; set; }
        public geometrydetails geometry { get; set; }
        public epsg epsgcode { get; set; }
    }
}
