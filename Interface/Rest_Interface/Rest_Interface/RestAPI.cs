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
    // Klasse für das Abfertigen von HTTP Requests und Responses
    class RestAPI
    {
        // Die REST Url muss nicht dezitiert angegeben werden müssen wenn der Service auf einem Webserver installiert ist
        const string BaseUrl = "http://localhost:8000";

        public RestAPI() {
        }


        public T Execute<T>(RestRequest request) where T : new()
        {
            // Führt den Request aus
            var client = new RestClient();
            client.BaseUrl = new Uri(BaseUrl);
            var response = client.Execute<T>(request);

            // Wenn was schief geht ...
            if (response.ErrorException != null)
            {
                const string message = "Somthing went terribly wrong with your request";
                var apiException = new ApplicationException(message, response.ErrorException);
                throw apiException;
            }

            return response.Data;
        }

        // Methode für das Anfordern der Provider
        public servicelist getServices()
        {
            var request = new RestRequest(Method.GET);
            request.Resource = "/getGeoCodingProviders";

            request.RequestFormat = DataFormat.Json;

            // führt Request aus und Deserialisiert gemäß der hinterlegten Klassenstruktur
            servicelist responselist = Execute<servicelist>(request);

            return responselist; 
        }

        // Methode für das direkte Geocoding - Anfordern von Koordinaten zu einer Adresse
        public codingObject getCoordinates(string provider, codingObject adressobject)
        {
            
            var request = new RestRequest(Method.GET);
            request.Resource = "/getCoords";
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddParameter("provider", provider); // Provider wird direkt übermittelt
            request.AddParameter("properties", JsonConvert.SerializeObject(adressobject.properties)); // es wird nur das Properties'objekt' übermittelt
            request.RequestFormat = DataFormat.Json;

            codingObject codinglist = Execute<codingObject>(request);

            return codinglist;
        }

        // Methode für das reverse Geocoding - Anfordern einer Adresse zu Koordinaten
        public codingObject getAdress(string provider, codingObject adressobject)
        {
            adressobject.geometry.type = "Point";

            var request = new RestRequest(Method.GET);
            request.Resource = "/getAddress";
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddParameter("provider", provider); // Provider wird direkt übermittelt
            request.AddParameter("geometry", JsonConvert.SerializeObject(adressobject.geometry)); // es wird nur das Geometry'objekt' übermittelt
            request.RequestFormat = DataFormat.Json;

            codingObject codinglist = Execute<codingObject>(request);

            return codinglist;
        }

        // wenn man nur die Adressen retour haben möchte
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
