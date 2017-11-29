﻿    using System;

    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Runtime;
    using Android.Views;
    using Android.Widget;
    using Android.OS;

    using Android.Support.V7.App;
    using Android.Gms.Common.Apis;
    using System.Collections.Generic;
    using Android.Gms.Location;
    using Android.Media;
    using Android.Util;
    using Java.Lang;
using ecUAQ.Models;
using Android.Gms.Maps.Model;

namespace ecUAQ.Droid
    {
    [Activity(Label = "Cultura UAQ", Icon = "@drawable/logo_proyecto", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity,
            GoogleApiClient.IConnectionCallbacks,
            GoogleApiClient.IOnConnectionFailedListener
        {
            protected const string TAG = "BBB";
            protected GoogleApiClient mGoogleApiClient;
            protected IList<IGeofence> mGeofenceList;
            bool mGeofencesAdded;
            PendingIntent mGeofencePendingIntent;
            ISharedPreferences mSharedPreferences;

            protected override void OnCreate(Bundle savedInstanceState)
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(savedInstanceState);

                mGeofenceList = new List<IGeofence>();
                mGeofencePendingIntent = null;
                //RemoveGeofencesButtonHandler();
                //AddGeofencesButtonHandler();
                mGeofenceList = new List<IGeofence>();
                mGeofencePendingIntent = null;
                Log.Info(TAG, "inicia");
                mSharedPreferences = GetSharedPreferences(Constants.SHARED_PREFERENCES_NAME,
                    FileCreationMode.Private);
                Log.Info(TAG, "mSharedPreferences");

                mGeofencesAdded = mSharedPreferences.GetBoolean(Constants.GEOFENCES_ADDED_KEY, false);
                Log.Info(TAG, "mGeofencesAdded");

                PopulateGeofenceList();
                Log.Info(TAG, "PopulateGeofenceList");
                BuildGoogleApiClient();
                Log.Info(TAG, "BuildGoogleApiClient");
                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
                
                XamForms.Controls.Droid.Calendar.Init();  // <<<<<!!! Please insert this
                LoadApplication(new App());
                Log.Info(TAG, "LoadApplication");
                //RemoveGeofencesButtonHandler();

            }

            protected void BuildGoogleApiClient()
            {
                Log.Info(TAG, "BuildGoogleApiClient");
                mGoogleApiClient = new GoogleApiClient.Builder(this)
                    .AddConnectionCallbacks(this)
                    .AddOnConnectionFailedListener(this)
                    .AddApi(LocationServices.API)
                    .Build();
            }

            protected override void OnStart()
            {
                Log.Info(TAG, "OnStart");
                base.OnStart();
                mGoogleApiClient.Connect();

            }

            protected override void OnStop()
            {
                Log.Info(TAG, "OnStop");
                base.OnStop();
                mGoogleApiClient.Disconnect();
            }

            public void OnConnected(Bundle connectionHint)
            {
                Log.Info(TAG, "Connected to GoogleApiClient");
                AddGeofencesButtonHandler();
                Log.Info(TAG, "AddGeofencesButtonHandler");
            }

            public void OnConnectionSuspended(int cause)
            {
                Log.Info(TAG, "Connection suspended");
            }

            public void OnConnectionFailed(Android.Gms.Common.ConnectionResult result)
            {
                Log.Info(TAG, "Connection failed: ConnectionResult.getErrorCode() = " + result.ErrorCode);
            }

            GeofencingRequest GetGeofencingRequest()
            {
                Log.Info(TAG, "GetGeofencingRequest");
                var builder = new GeofencingRequest.Builder();
                builder.SetInitialTrigger(GeofencingRequest.InitialTriggerEnter);
                builder.AddGeofences(mGeofenceList);

                return builder.Build();
            }

            public async void AddGeofencesButtonHandler()
            {
                Log.Info(TAG, "AddGeofencesButtonHandler");
                if (!mGoogleApiClient.IsConnected)
                {
                    Log.Info(TAG, "mGoogleApiClient.IsConnected");
                    Toast.MakeText(this, GetString(Resource.String.not_connected), ToastLength.Short).Show();
                    return;
                }

                try
                {
                    var status = await LocationServices.GeofencingApi.AddGeofencesAsync(mGoogleApiClient, GetGeofencingRequest(),
                        GetGeofencePendingIntent());
                    HandleResult(status);
                }
                catch (SecurityException securityException)
                {
                    Log.Info(TAG, "SecurityException");
                    LogSecurityException(securityException);
                }
            }

            public async void RemoveGeofencesButtonHandler()
            {
                Log.Info(TAG, "RemoveGeofencesButtonHandler");
                if (!mGoogleApiClient.IsConnected)
                {
                    Log.Info(TAG, "mGoogleApiClient.IsConnected");
                    Toast.MakeText(this, GetString(Resource.String.not_connected), ToastLength.Short).Show();
                    return;
                }
                try
                {
                    var status = await LocationServices.GeofencingApi.RemoveGeofencesAsync(mGoogleApiClient,
                        GetGeofencePendingIntent());
                    HandleResult(status);
                }
                catch (SecurityException securityException)
                {
                    Log.Info(TAG, "SecurityException");
                    LogSecurityException(securityException);
                }
            }

            void LogSecurityException(SecurityException securityException)
            {
                Log.Error(TAG, "Invalid location permission. " +
                    "You need to use ACCESS_FINE_LOCATION with geofences", securityException);
            }

            public void HandleResult(Statuses status)
            {
                Log.Info(TAG, "HandleResult");
                if (status.IsSuccess)
                {
                    Log.Info(TAG, "status.IsSuccess");
                    mGeofencesAdded = !mGeofencesAdded;
                    var editor = mSharedPreferences.Edit();
                    editor.PutBoolean(Constants.GEOFENCES_ADDED_KEY, mGeofencesAdded);
                    editor.Commit();


                    Toast.MakeText(
                        this,
                        GetString(mGeofencesAdded ? Resource.String.geofences_added :
                            Resource.String.geofences_removed),
                        ToastLength.Short
                    ).Show();
                }
                else
                {
                    var errorMessage = GeofenceErrorMessages.GetErrorString(this,
                        status.StatusCode);
                    Log.Error(TAG, errorMessage);
                }
            }

            PendingIntent GetGeofencePendingIntent()
            {
                Log.Info(TAG, "GetGeofencePendingIntent");
                if (mGeofencePendingIntent != null)
                {
                    return mGeofencePendingIntent;
                }
                var intent = new Intent(this, typeof(GeofenceTransitionsIntentService));
                return PendingIntent.GetService(this, 0, intent, PendingIntentFlags.UpdateCurrent);
            }

            public async void PopulateGeofenceList()
            {
                RestClient cliente = new RestClient();
                var eventos = await cliente.Get2<ListaEventos>("http://189.211.201.181:86/CulturaUAQWebservice/api/tbleventos");
                if (eventos != null)
                {
                    if (eventos.listaEventos.Count > 0)
                    {
                        foreach (var evento in eventos.listaEventos)
                        {
                            Constants.BAY_AREA_LANDMARKS.Add(evento.titulo, new LatLng(System.Double.Parse(evento.latitud), System.Double.Parse(evento.longitud)));
                        }
                    }
                }
                Log.Info(TAG, "PopulateGeofenceList");
                foreach (var entry in Constants.BAY_AREA_LANDMARKS)
                {
                    System.Diagnostics.Debug.Write("titulo");
                    System.Diagnostics.Debug.Write(entry.Key);
                    mGeofenceList.Add(new GeofenceBuilder()
                        .SetRequestId(entry.Key)
                        .SetCircularRegion(
                            entry.Value.Latitude,
                            entry.Value.Longitude,
                            Constants.GEOFENCE_RADIUS_IN_METERS
                        )
                        .SetExpirationDuration(Constants.GEOFENCE_EXPIRATION_IN_MILLISECONDS)
                        .SetTransitionTypes(Geofence.GeofenceTransitionEnter |
                            Geofence.GeofenceTransitionExit)
                        .Build());
                }
            }

            protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
            {
                base.OnActivityResult(requestCode, resultCode, data);
            }

        }

    }
