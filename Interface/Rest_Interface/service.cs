using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace GeoCodingInterface
{
    class service
    {
        public string name { get; set; }
  

    public List<service> getServices() 
    {
        List<service> available = new List<service>();
        return available;
    }

    }

    public class serviceResponse
    {
        public List<service> service { get; set; }
    }

    public class services : IRestResponse<serviceResponse> 
    {
    
    }
}
