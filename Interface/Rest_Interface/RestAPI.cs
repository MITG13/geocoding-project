using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers;
using Newtonsoft.Json;

namespace GeoCodingInterface
{
    class RestAPI
    {
        const string BaseUrl = "http://localhost:8000";

        public RestAPI() {
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new Uri(BaseUrl);
            var response = client.Execute<T>(request);
            Console.WriteLine(response.Content);

            if (response.ErrorException != null)
            {
                const string message = "Somthing went terribly wrong with your request";
                var apiException = new ApplicationException(message, response.ErrorException);
                throw apiException;
            }

            return response.Data;
        }

        public servicelist getServices()
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "/getGeoCodingProviders";

            request.RequestFormat = DataFormat.Json;

            servicelist responselist = Execute<servicelist>(request);

            return responselist; 
        }

        public codingObject getCoordinates(string provider, codingObject adressobject)
        {
            
            var request = new RestRequest(Method.GET);
            request.Resource = "/getCoords";
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddParameter("provider", provider);
            request.AddParameter("properties", JsonConvert.SerializeObject(adressobject.properties));
            request.RequestFormat = DataFormat.Json;

            codingObject codinglist = Execute<codingObject>(request);

            return codinglist;
        }

        public codingObject getAdress(string provider, codingObject adressobject)
        {
            adressobject.geometry.type = "Point";

            var request = new RestRequest(Method.GET);
            request.Resource = "/getAddress";
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddParameter("provider", provider);
            request.AddParameter("geometry", JsonConvert.SerializeObject(adressobject.geometry));
            request.RequestFormat = DataFormat.Json;

            codingObject codinglist = Execute<codingObject>(request);

            return codinglist;
        }

        public double[] getCoordinates(codingObject codinglist)
        {
            double[] coordinates = new double[2];
            List<string> stringcoord = codinglist.geometry.coordinates;
            coordinates[0] = Convert.ToDouble(stringcoord[0]);
            coordinates[1] = Convert.ToDouble(stringcoord[1]);
            return coordinates;
        }


    }
}
