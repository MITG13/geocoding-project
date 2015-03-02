using RestSharp;
using RestSharp.Serializers;
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



        static void Main(string[] args)
        {



            RestAPI test = new RestAPI();

            servicelist providers = test.getServices();
            codingObject myadress = new codingObject();

            myadress.properties = new addressdata();
            myadress.properties.address = "sonnbergstraße 58, 2380 Austria";

            codingObject thoseCoords = test.getCoordinates(providers.providers[0], myadress);

            myadress.geometry = new geometrydetails();
            myadress.geometry.coordinates = new List<string>();
            myadress.geometry.coordinates.Add("48.1263169");
            myadress.geometry.coordinates.Add("16.2576945");

            thoseCoords = test.getAdress(providers.providers[0], myadress);

            Console.ReadKey();



        }

    }
}
