using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GeoCodingInterface
{
    class GeoCodingInterface
    {

        private Uri uriAddress = new Uri("http://node.js");


        public List<service> GetServices()
        {

            List<service> available = new List<service>();
            var client = new RestClient();

            client.BaseUrl = uriAddress;

            var request = new RestRequest(Method.GET);

            request.Resource = "rest?";
            request.AddParameter("key", "value");

            request.RequestFormat = DataFormat.Json;
            
            var response = client.Execute<serviceResponse>(request);

            available = response.Data.service;

            return available;
            
        }

        public coordinates GetLatLng()
        {

            coordinates latlng = new coordinates();
            var client = new RestClient();

            client.BaseUrl = uriAddress;

            var request = new RestRequest(Method.POST);

            request.Resource = "rest?";
            request.AddParameter("key", "value");

            request.RequestFormat = DataFormat.Json;

            var response = client.Execute<codingResponse>(request);

            latlng = response.Data.latlng;

            return latlng;

        }

        /*
        static void Main(string[] args)
        {

        }
        */
    }
}
