using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCodingInterface
{
    
    // Klasse für das deserialisieren des ganzen JSON Objects. Entspricht dem zurückgelieferten Objekt der getAdress bzw. getCoordinates Request
    class codingObject
    {
        public addressdata properties { get; set; }
        public geometrydetails geometry { get; set; }
        public epsg epsgcode { get; set; }
    }
}
