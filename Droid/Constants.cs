using System.Collections.Generic;
using Android.Gms.Maps.Model;

namespace ecUAQ.Droid
{
    public static class Constants
    {
        public const string PACKAGE_NAME = "com.andradescompany.ecUAQ";
        public const string SHARED_PREFERENCES_NAME = PACKAGE_NAME + ".SHARED_PREFERENCES_NAME";
        public const string GEOFENCES_ADDED_KEY = PACKAGE_NAME + ".GEOFENCES_ADDED_KEY";

        public const long GEOFENCE_EXPIRATION_IN_HOURS = 12;
        public const long GEOFENCE_EXPIRATION_IN_MILLISECONDS = GEOFENCE_EXPIRATION_IN_HOURS * 60 * 60 * 1000;
        public const float GEOFENCE_RADIUS_IN_METERS = 1000;

        public static readonly Dictionary<string, LatLng> BAY_AREA_LANDMARKS = new Dictionary<string, LatLng> {
            { "PJEM", new LatLng (19.2920, -99.647427) },
            { "World Trade Center", new LatLng (19.393664, -99.1745978) },
            { "Aeropuerto CDMX", new LatLng (19.4360762, -99.074097) },
            { "Arizoba", new LatLng (19.3933049, -99.181077) }
        };
    }
}

