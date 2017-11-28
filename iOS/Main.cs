using System;
using System.Collections.Generic;
using System.Linq;
using CoreLocation;
using Foundation;
using UIKit;

namespace ecUAQ.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            /*Inicia la geolocalizacion*/
            inicializaGeolocalizacion();
            Console.Write("Entro.............................................");
            /*Inicia la geolocalizacion*/
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }

        static void inicializaGeolocalizacion(){
            /*Se crean las variables de geolocalizacion*/
            CLGeocoder geocoder = new CLGeocoder();
            //CLCircularRegion region;19.285116, -99.675914
            CLCircularRegion region = new CLCircularRegion(new CLLocationCoordinate2D(+19.285116, -99.675914), 100129.46, "Casa de toño");//19.273600, -99.675620
            CLLocationManager locMan;
            /*Se crean las variables de geolocalizacion*/


            locMan = new CLLocationManager();
            locMan.RequestWhenInUseAuthorization();
            locMan.RequestAlwaysAuthorization();
            // Geocode a city to get a CLCircularRegion,
            // and then use our location manager to set up a geofence

            // clean up monitoring of old region so they don't pile up
            Console.Write("Soy la region");
            Console.Write(region);
            Console.Write("termino soy la region");
            if (region != null)
            {
                locMan.StopMonitoring(region);
            }

            // Geocode city location to create a CLCircularRegion - what we need for geofencing!
            var taskCoding = geocoder.GeocodeAddressAsync("Cupertino");
            taskCoding.ContinueWith((addresses) => {
                CLPlacemark placemark = addresses.Result[0];
                region = (CLCircularRegion)placemark.Region;
                Console.Write("\nInicio el monitoreo ..........");
                locMan.StartMonitoring(region);
                Console.Write("\nTermino el monitoreo ..........");
            });


            // This gets called even when the app is in the background - try it!
            locMan.RegionEntered += (sender, e) => {
                Console.WriteLine("You've entered the region");
            };

            locMan.RegionLeft += (sender, e) => {
                Console.WriteLine("You've left the region");
            };
        }
    }
}
